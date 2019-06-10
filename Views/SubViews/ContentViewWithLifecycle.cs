// *********************************************************************************
// <copyright file=ContentViewWithLifecycle.cs company="Marcus Technical Services, Inc.">
//     Copyright @2019 Marcus Technical Services, Inc.
// </copyright>
//
// MIT License
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NON-INFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
// *********************************************************************************

namespace Com.MarcusTS.LifecycleAware.Views.SubViews
{
   using Com.MarcusTS.LifecycleAware.Common.Interfaces;
   using Com.MarcusTS.LifecycleAware.Common.Utils;
   using Com.MarcusTS.SharedForms.Common.Utils;
   using Com.MarcusTS.SharedUtils.Utils;
   using System;
   using Xamarin.Forms;

   /// <summary>
   ///    Interface IContentViewWithLifecycle
   ///    Implements the <see cref="IHostAppLifecycleReporter" />
   ///    Implements the <see cref="IHostPageLifecycleReporter" />
   ///    Implements the <see cref="ICleanUpBeforeFinalization" />
   ///    Implements the <see cref="Com.MarcusTS.LifecycleAware.Common.Interfaces.IHostAppLifecycleReporter" />
   ///    Implements the <see cref="Com.MarcusTS.LifecycleAware.Common.Interfaces.IHostPageLifecycleReporter" />
   ///    Implements the <see cref="Com.MarcusTS.LifecycleAware.Common.Interfaces.ICleanUpBeforeFinalization" />
   ///    Implements the <see cref="Com.MarcusTS.LifecycleAware.Common.Interfaces.ICanDisappearByForce" />
   ///    Implements the <see cref="Com.MarcusTS.LifecycleAware.Common.Interfaces.IHostStageLifecycleReporter" />
   ///    Implements the <see cref="System.IDisposable" />
   /// </summary>
   /// <seealso cref="Com.MarcusTS.LifecycleAware.Common.Interfaces.IHostStageLifecycleReporter" />
   /// <seealso cref="System.IDisposable" />
   /// <seealso cref="Com.MarcusTS.LifecycleAware.Common.Interfaces.ICanDisappearByForce" />
   /// <seealso cref="Com.MarcusTS.LifecycleAware.Common.Interfaces.IHostAppLifecycleReporter" />
   /// <seealso cref="Com.MarcusTS.LifecycleAware.Common.Interfaces.IHostPageLifecycleReporter" />
   /// <seealso cref="Com.MarcusTS.LifecycleAware.Common.Interfaces.ICleanUpBeforeFinalization" />
   /// <seealso cref="IHostAppLifecycleReporter" />
   /// <seealso cref="IHostPageLifecycleReporter" />
   /// <seealso cref="ICleanUpBeforeFinalization" />
   public interface IContentViewWithLifecycle : IHostAppLifecycleReporter, IHostPageLifecycleReporter,
                                                IHostStageLifecycleReporter,
                                                ICleanUpBeforeFinalization, ICanDisappearByForce, IDisposable
   {
   }

   /// <summary>
   ///    Use this as the basis of all views if possible. If not possible in a few cases, copy this code into your other
   ///    classes as-is and it will work the same way.
   ///    Implements the <see cref="Xamarin.Forms.ContentView" />
   ///    Implements the <see cref="IContentViewWithLifecycle" />
   ///    Implements the <see cref="Com.MarcusTS.LifecycleAware.Views.SubViews.IContentViewWithLifecycle" />
   ///    Implements the <see cref="ContentView" />
   /// </summary>
   /// <seealso cref="ContentView" />
   /// <seealso cref="Com.MarcusTS.LifecycleAware.Views.SubViews.IContentViewWithLifecycle" />
   /// <seealso cref="Xamarin.Forms.ContentView" />
   /// <seealso cref="IContentViewWithLifecycle" />
   /// <remarks>
   ///    REMEMBER to supply the originating page (PageLifecycleReporterProperty), as that is how all of this works. The
   ///    event ties are weak and therefore non-binding.
   /// </remarks>
   public class ContentViewWithLifecycle : ContentView, IContentViewWithLifecycle
   {
      /// <summary>
      ///    The application lifecycle reporter property
      /// </summary>
      public static BindableProperty AppLifecycleReporterProperty =
         CreateContentViewWithLifecycleBindableProperty
         (
            nameof(AppLifecycleReporter),
            default(IReportAppLifecycle),
            BindingMode.OneWay,
            (
               contentView,
               oldVal,
               newVal
            ) =>
            {
               contentView.AppLifecycleReporter = newVal;
            }
         );

      /// <summary>
      ///    The page lifecycle reporter property
      /// </summary>
      public static BindableProperty PageLifecycleReporterProperty =
         CreateContentViewWithLifecycleBindableProperty
         (
            nameof(PageLifecycleReporter),
            default(IReportPageLifecycle),
            BindingMode.OneWay,
            (
               contentView,
               oldVal,
               newVal
            ) =>
            {
               contentView.PageLifecycleReporter = newVal;
            }
         );

      /// <summary>
      ///    The application lifecycle reporter
      /// </summary>
      private IReportAppLifecycle _appLifecycleReporter;

      /// <summary>
      ///    The is cleaning up
      /// </summary>
      private bool _isCleaningUp;

      /// <summary>
      ///    The lifecycle reporter
      /// </summary>
      private IReportPageLifecycle _pageLifecycleReporter;

      /// <summary>
      ///    The stage lifecycle reporter
      /// </summary>
      private IReportStageLifecycle _stageLifecycleReporter;

      /// <summary>
      ///    Initializes a new instance of the <see cref="ContentViewWithLifecycle" /> class.
      /// </summary>
      public ContentViewWithLifecycle()
      {
         this.SetBindingContextLifecycleReporters();
      }

      /// <summary>
      ///    Gets or sets the content of the ContentView.
      /// </summary>
      /// <value>A <see cref="T:Xamarin.Forms.View" /> that contains the content.</value>
      public new View Content
      {
         get => base.Content;
         set
         {
            base.Content = value;
            SetUpNewContent();
         }
      }

      /// <summary>
      ///    Gets or sets the application lifecycle reporter.
      /// </summary>
      /// <value>The application lifecycle reporter.</value>
      public IReportAppLifecycle AppLifecycleReporter
      {
         get => _appLifecycleReporter;
         set => this.SetAppLifecycleReporter(ref _appLifecycleReporter, value);
      }

      /// <summary>
      ///    Gets or sets a value indicating whether this instance is cleaning up before finalization.
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
                  // Notifies the safe di container and other concerned foreign members
                  this.SendObjectDisappearingMessage();

                  // Notifies close relatives like view models
                  PageIsDisappearing?.Invoke(this);
               }
            }
         }
      }

      /// <summary>
      ///    Gets or sets the page lifecycle reporter.
      /// </summary>
      /// <value>The page lifecycle reporter.</value>
      public IReportPageLifecycle PageLifecycleReporter
      {
         get => _pageLifecycleReporter;
         set => this.SetPageLifecycleReporter(ref _pageLifecycleReporter, value);
      }

      /// <summary>
      ///    Gets or sets the stage lifecycle reporter.
      /// </summary>
      /// <value>The stage lifecycle reporter.</value>
      public IReportStageLifecycle StageLifecycleReporter
      {
         get => _stageLifecycleReporter;
         set => this.SetStageLifecycleReporter(ref _stageLifecycleReporter, value);
      }

      /// <summary>
      ///    Disposes this instance.
      /// </summary>
      public void Dispose()
      {
         Dispose(true);
         GC.SuppressFinalize(this);
      }

      /// <summary>
      ///    Forces the disappearing.
      /// </summary>
      public void ForceDisappearing()
      {
         Disappear();
      }

      /// <summary>
      ///    Called when [application going to sleep].
      /// </summary>
      public virtual void OnAppGoingToSleep()
      {
      }

      /// <summary>
      ///    Called when [application resuming].
      /// </summary>
      public virtual void OnAppResuming()
      {
      }

      /// <summary>
      ///    Called when [application starting].
      /// </summary>
      public virtual void OnAppStarting()
      {
      }

      /// <summary>
      ///    Called when the page disappears.
      /// </summary>
      public virtual void OnPageAppearing()
      {
      }

      /// <summary>
      ///    Called when the page disappears.
      /// </summary>
      public virtual void OnPageDisappearing()
      {
         TryCleaningUpBeforeFinalization();
      }

      /// <summary>
      ///    Called when [stage appearing].
      /// </summary>
      public virtual void OnStageAppearing()
      {
      }

      /// <summary>
      ///    Called when [stage disappearing].
      /// </summary>
      public virtual void OnStageDisappearing()
      {
      }

      /// <summary>
      ///    Finalizes an instance of the <see cref="ContentViewWithLifecycle" /> class.
      /// </summary>
      ~ContentViewWithLifecycle()
      {
         Dispose(false);
      }

      /// <summary>
      ///    Occurs when [page is disappearing].
      /// </summary>
      public event EventUtils.GenericDelegate<object> PageIsDisappearing;

      /// <summary>
      ///    Creates the content view with lifecycle bindable property.
      /// </summary>
      /// <typeparam name="PropertyTypeT">The type of the property type t.</typeparam>
      /// <param name="localPropName">Name of the local property.</param>
      /// <param name="defaultVal">The default value.</param>
      /// <param name="bindingMode">The binding mode.</param>
      /// <param name="callbackAction">The callback action.</param>
      /// <returns>BindableProperty.</returns>
      public static BindableProperty CreateContentViewWithLifecycleBindableProperty<PropertyTypeT>
      (
         string localPropName,
         PropertyTypeT
            defaultVal =
            default,
         BindingMode
            bindingMode =
            BindingMode
               .OneWay,
         Action<
               ContentViewWithLifecycle
             , PropertyTypeT,
               PropertyTypeT>
            callbackAction =
            null
      )
      {
         return BindableUtils.CreateBindableProperty(localPropName, defaultVal, bindingMode, callbackAction);
      }

      /// <summary>
      ///    Disappears this instance.
      /// </summary>
      protected void Disappear()
      {
         PageIsDisappearing?.Invoke(this);

         this.SendObjectDisappearingMessage();
      }

      /// <summary>
      ///    Releases unmanaged and - optionally - managed resources.
      /// </summary>
      /// <param name="disposing">
      ///    <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only
      ///    unmanaged resources.
      /// </param>
      protected virtual void Dispose(bool disposing)
      {
         ReleaseUnmanagedResources();
         if (disposing)
         {
         }
      }

      /// <summary>
      ///    Releases the unmanaged resources.
      /// </summary>
      protected virtual void ReleaseUnmanagedResources()
      {
         TryCleaningUpBeforeFinalization();
      }

      /// <summary>
      ///    Sets up new content.
      /// </summary>
      protected virtual void SetUpNewContent()
      {
         base.Content.SetLifecycleReporters(this);
      }

      /// <summary>
      ///    Tries the cleaning up before finalization.
      /// </summary>
      protected virtual void TryCleaningUpBeforeFinalization()
      {
         if (!IsCleaningUpBeforeFinalization)
         {
            IsCleaningUpBeforeFinalization = true;
         }
      }
   }
}