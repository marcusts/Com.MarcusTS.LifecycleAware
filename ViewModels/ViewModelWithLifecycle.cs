#region License

// Copyright (c) 2019  Marcus Technical Services, Inc. <marcus@marcusts.com>
//
// This file, ViewModelWithLifecycle.cs, is a part of a program called AccountViewMobile.
//
// AccountViewMobile is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// Permission to use, copy, modify, and/or distribute this software
// for any purpose with or without fee is hereby granted, provided
// that the above copyright notice and this permission notice appear
// in all copies.
//
// AccountViewMobile is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// For the complete GNU General Public License,
// see <http://www.gnu.org/licenses/>.

#endregion

namespace Com.MarcusTS.LifecycleAware.ViewModels
{
   using Common.Interfaces;
   using Common.Utils;
   using System;
   using System.ComponentModel;
   using System.Runtime.CompilerServices;

   /// <summary>
   /// Interface IViewModelWithLifecycle
   /// Implements the <see cref="System.ComponentModel.INotifyPropertyChanged" />
   /// Implements the <see cref="INotifyPropertyChanged" />
   /// Implements the <see cref="System.ComponentModel" />
   /// Implements the <see cref="System" />
   /// Implements the <see cref="System.ComponentModel.INotifyPropertyChanged" />
   /// Implements the <see cref="Com.MarcusTS.LifecycleAware.Common.Interfaces.IHostAppLifecycleReporter" />
   /// Implements the <see cref="Com.MarcusTS.LifecycleAware.Common.Interfaces.IHostPageLifecycleReporter" />
   /// Implements the <see cref="Com.MarcusTS.LifecycleAware.Common.Interfaces.ICleanUpBeforeFinalization" />
   /// Implements the <see cref="INotifyPropertyChanged" />
   /// Implements the <see cref="Com.MarcusTS.LifecycleAware.Common.Interfaces.IHostStageLifecycleReporter" />
   /// Implements the <see cref="System.IDisposable" />
   /// </summary>
   /// <seealso cref="Com.MarcusTS.LifecycleAware.Common.Interfaces.IHostStageLifecycleReporter" />
   /// <seealso cref="System.IDisposable" />
   /// <seealso cref="INotifyPropertyChanged" />
   /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
   /// <seealso cref="Com.MarcusTS.LifecycleAware.Common.Interfaces.IHostAppLifecycleReporter" />
   /// <seealso cref="Com.MarcusTS.LifecycleAware.Common.Interfaces.IHostPageLifecycleReporter" />
   /// <seealso cref="Com.MarcusTS.LifecycleAware.Common.Interfaces.ICleanUpBeforeFinalization" />
   /// <seealso cref="ICleanUpBeforeFinalization" />
   /// <seealso cref="IHostAppLifecycleReporter" />
   /// <seealso cref="IHostPageLifecycleReporter" />
   /// <seealso cref="IHostPageLifecycleReporter" />
   public interface IViewModelWithLifecycle : INotifyPropertyChanged, IHostAppLifecycleReporter,
                                              IHostStageLifecycleReporter,
                                              IHostPageLifecycleReporter, ICleanUpBeforeFinalization, IDisposable
   {
   }

   /// <summary>
   /// Use this as the basis of all view models if possible. If not possible in a few cases, copy this code into your
   /// other classes as-is and it will work the same way.
   /// Implements the <see cref="IViewModelWithLifecycle" />
   /// Implements the <see cref="Com.MarcusTS.LifecycleAware.ViewModels.IViewModelWithLifecycle" />
   /// Implements the <see cref="object" />
   /// </summary>
   /// <seealso cref="object" />
   /// <seealso cref="Com.MarcusTS.LifecycleAware.ViewModels.IViewModelWithLifecycle" />
   /// <seealso cref="IViewModelWithLifecycle" />
   /// <remarks>REMEMBER to set the <see cref="AppLifecycleReporter" /> to the current app and the
   /// <see cref="PageLifecycleReporter" /> to the parent page. The event ties are weak and non-binding.</remarks>
   public class ViewModelWithLifecycle : IViewModelWithLifecycle
   {
      /// <summary>
      /// The application lifecycle reporter
      /// </summary>
      private IReportAppLifecycle _appLifecycleReporter;

      /// <summary>
      /// The is cleaning up
      /// </summary>
      private bool _isCleaningUp;

      /// <summary>
      /// The page lifecycle reporter
      /// </summary>
      private IReportPageLifecycle _pageLifecycleReporter;

      /// <summary>
      /// The stage lifecycle reporter
      /// </summary>
      private IReportStageLifecycle _stageLifecycleReporter;

      /// <summary>
      /// Occurs when [property changed].
      /// </summary>
      public event PropertyChangedEventHandler PropertyChanged;

      /// <summary>
      /// Gets or sets the application lifecycle reporter.
      /// </summary>
      /// <value>The application lifecycle reporter.</value>
      public IReportAppLifecycle AppLifecycleReporter
      {
         get => _appLifecycleReporter;
         set => this.SetAppLifecycleReporter(ref _appLifecycleReporter, value);
      }

      /// <summary>
      /// Gets or sets a value indicating whether this instance is cleaning up before finalization.
      /// </summary>
      /// <value><c>true</c> if this instance is cleaning up before finalization; otherwise, <c>false</c>.</value>
      public bool IsCleaningUpBeforeFinalization
      {
         get => _isCleaningUp;
         set
         {
            if (_isCleaningUp != value)
            {
               _isCleaningUp = value;

               if (_isCleaningUp)
               {
                  OnIsCleaningUpBeforeFinalization();

                  // Notifies the safe di container and other concerned foreign members
                  this.SendObjectDisappearingMessage();
               }
            }
         }
      }

      /// <summary>
      /// Gets or sets the page lifecycle reporter.
      /// </summary>
      /// <value>The page lifecycle reporter.</value>
      public IReportPageLifecycle PageLifecycleReporter
      {
         get => _pageLifecycleReporter;
         set => this.SetPageLifecycleReporter(ref _pageLifecycleReporter, value);
      }

      /// <summary>
      /// Gets or sets the stage lifecycle reporter.
      /// </summary>
      /// <value>The stage lifecycle reporter.</value>
      public IReportStageLifecycle StageLifecycleReporter
      {
         get => _stageLifecycleReporter;
         set => this.SetStageLifecycleReporter(ref _stageLifecycleReporter, value);
      }

      /// <summary>
      /// Disposes this instance.
      /// </summary>
      public void Dispose()
      {
         Dispose(true);
         GC.SuppressFinalize(this);
      }

      /// <summary>
      /// Called when [application going to sleep].
      /// </summary>
      void IHostAppLifecycleReporter.OnAppGoingToSleep()
      {
         OnAppGoingToSleep();
      }

      /// <summary>
      /// Called when [application resuming].
      /// </summary>
      void IHostAppLifecycleReporter.OnAppResuming()
      {
         OnAppResuming();
      }

      /// <summary>
      /// Called when [application starting].
      /// </summary>
      void IHostAppLifecycleReporter.OnAppStarting()
      {
         OnAppStarting();
      }

      /// <summary>
      /// Called when [page appearing].
      /// </summary>
      /// <exception cref="NotImplementedException"></exception>
      public virtual void OnPageAppearing()
      {
      }

      /// <summary>
      /// Called when [page disappearing].
      /// </summary>
      /// <exception cref="NotImplementedException"></exception>
      public virtual void OnPageDisappearing()
      {
      }

      /// <summary>
      /// Called when [stage appearing].
      /// </summary>
      public virtual void OnStageAppearing()
      {
      }

      /// <summary>
      /// Called when [stage disappearing].
      /// </summary>
      public virtual void OnStageDisappearing()
      {
      }

      /// <summary>
      /// Finalizes an instance of the <see cref="ViewModelWithLifecycle" /> class.
      /// </summary>
      ~ViewModelWithLifecycle()
      {
         if (!IsCleaningUpBeforeFinalization)
         {
            IsCleaningUpBeforeFinalization = true;
         }
      }

      /// <summary>
      /// Releases unmanaged and - optionally - managed resources.
      /// </summary>
      /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only
      /// unmanaged resources.</param>
      protected virtual void Dispose(bool disposing)
      {
         ReleaseUnmanagedResources();
         if (disposing)
         {
         }
      }

      /// <summary>
      /// Called when [application going to sleep].
      /// </summary>
      protected virtual void OnAppGoingToSleep()
      {
      }

      /// <summary>
      /// Called when [application resuming].
      /// </summary>
      protected virtual void OnAppResuming()
      {
      }

      /// <summary>
      /// Called when [application starting].
      /// </summary>
      protected virtual void OnAppStarting()
      {
      }

      /// <summary>
      /// Called when [is cleaning up before finalization].
      /// </summary>
      protected virtual void OnIsCleaningUpBeforeFinalization()
      {
      }

      /// <summary>
      /// Called when [page appearing].
      /// </summary>
      /// <param name="val">The value.</param>
      protected virtual void OnPageAppearing(object val)
      {
      }

      /// <summary>
      /// Called when [page disappearing].
      /// </summary>
      /// <param name="val">The value.</param>
      protected virtual void OnPageDisappearing(object val)
      {
         IsCleaningUpBeforeFinalization = true;
      }

      /// <summary>
      /// Called when [property changed].
      /// </summary>
      /// <param name="propertyName">Name of the property.</param>
      protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
      {
         PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
      }

      /// <summary>
      /// Releases the unmanaged resources.
      /// </summary>
      protected virtual void ReleaseUnmanagedResources()
      {
      }
   }
}