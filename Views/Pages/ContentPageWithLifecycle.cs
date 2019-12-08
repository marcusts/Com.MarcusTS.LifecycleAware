#region License

// Copyright (c) 2019  Marcus Technical Services, Inc. <marcus@marcusts.com>
//
// This file, ContentPageWithLifecycle.cs, is a part of a program called AccountViewMobile.
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

namespace Com.MarcusTS.LifecycleAware.Views.Pages
{
   using Common.Interfaces;
   using Common.Utils;
   using SharedForms.Common.Utils;
   using SharedUtils.Utils;
   using System;
   using Xamarin.Forms;

   /// <summary>
   /// Interface IContentPageWithLifecycle
   /// Implements the <see cref="IHostAppLifecycleReporter" />
   /// Implements the <see cref="IHostAppLifecycleReporter" />
   /// Implements the <see cref="Com.MarcusTS.LifecycleAware.Common.Interfaces.IHostAppLifecycleReporter" />
   /// Implements the <see cref="Com.MarcusTS.LifecycleAware.Common.Interfaces.IReportPageLifecycle" />
   /// Implements the <see cref="Com.MarcusTS.LifecycleAware.Common.Interfaces.ICanDisappearByForce" />
   /// Implements the <see cref="System.IDisposable" />
   /// </summary>
   /// <seealso cref="System.IDisposable" />
   /// <seealso cref="Com.MarcusTS.LifecycleAware.Common.Interfaces.ICanDisappearByForce" />
   /// <seealso cref="Com.MarcusTS.LifecycleAware.Common.Interfaces.IHostAppLifecycleReporter" />
   /// <seealso cref="Com.MarcusTS.LifecycleAware.Common.Interfaces.IReportPageLifecycle" />
   /// <seealso cref="IReportPageLifecycle" />
   /// <seealso cref="IReportPageLifecycle" />
   /// <remarks>WARNING: .Net does not provide IContentPage, so references to this interface type *must* type-cast to
   /// ContentPage_LCA manually.</remarks>
   public interface IContentPageWithLifecycle : IHostAppLifecycleReporter, IReportPageLifecycle, ICanDisappearByForce,
                                                IDisposable
   {
   }

   /// <summary>
   /// Use this as the basis of all pages if possible. If not possible in a few cases, copy this code into your other
   /// classes as-is and it will work the same way.
   /// Implements the <see cref="Xamarin.Forms.ContentPage" />
   /// Implements the <see cref="IContentPageWithLifecycle" />
   /// Implements the <see cref="Com.MarcusTS.LifecycleAware.Views.Pages.IContentPageWithLifecycle" />
   /// Implements the <see cref="ContentPage" />
   /// </summary>
   /// <seealso cref="ContentPage" />
   /// <seealso cref="Com.MarcusTS.LifecycleAware.Views.Pages.IContentPageWithLifecycle" />
   /// <seealso cref="Xamarin.Forms.ContentPage" />
   /// <seealso cref="IContentPageWithLifecycle" />
   public class ContentPageWithLifecycle : ContentPage, IContentPageWithLifecycle
   {
      /// <summary>
      /// The application lifecycle reporter property
      /// </summary>
      public static BindableProperty AppLifecycleReporterProperty =
         CreateContentPageWithLifecycleBindableProperty
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
      /// The application lifecycle reporter
      /// </summary>
      private IReportAppLifecycle _appLifecycleReporter;

      /// <summary>
      /// Initializes a new instance of the <see cref="ContentPageWithLifecycle" /> class.
      /// </summary>
      public ContentPageWithLifecycle()
      {
         this.SetBindingContextLifecycleReporters();
      }

      /// <summary>
      /// Gets or sets the view that contains the content of the Page.
      /// </summary>
      /// <value>A <see cref="T:Xamarin.Forms.View" /> subclass, or <see langword="null" />.</value>
      /// <remarks>To be added.</remarks>
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
      /// Occurs when [page is appearing].
      /// </summary>
      public event EventUtils.GenericDelegate<object> PageIsAppearing;

      /// <summary>
      /// Occurs when [page is disappearing].
      /// </summary>
      public event EventUtils.GenericDelegate<object> PageIsDisappearing;

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
      /// Disposes this instance.
      /// </summary>
      public void Dispose()
      {
         Dispose(true);
         GC.SuppressFinalize(this);
      }

      /// <summary>
      /// Forces the disappearing.
      /// </summary>
      public void ForceDisappearing()
      {
         Disappear();
      }

      /// <summary>
      /// Called when [application going to sleep].
      /// </summary>
      public virtual void OnAppGoingToSleep()
      {
      }

      /// <summary>
      /// Called when [application resuming].
      /// </summary>
      public virtual void OnAppResuming()
      {
      }

      /// <summary>
      /// Called when [application starting].
      /// </summary>
      public virtual void OnAppStarting()
      {
      }

      /// <summary>
      /// Finalizes an instance of the <see cref="ContentPageWithLifecycle" /> class.
      /// </summary>
      ~ContentPageWithLifecycle()
      {
         Dispose(false);
      }

      /// <summary>
      /// Creates the content page with lifecycle bindable property.
      /// </summary>
      /// <typeparam name="PropertyTypeT">The type of the property type t.</typeparam>
      /// <param name="localPropName">Name of the local property.</param>
      /// <param name="defaultVal">The default value.</param>
      /// <param name="bindingMode">The binding mode.</param>
      /// <param name="callbackAction">The callback action.</param>
      /// <returns>BindableProperty.</returns>
      public static BindableProperty CreateContentPageWithLifecycleBindableProperty<PropertyTypeT>
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
               ContentPageWithLifecycle
             , PropertyTypeT,
               PropertyTypeT>
            callbackAction =
            null
      )
      {
         return BindableUtils.CreateBindableProperty(localPropName, defaultVal, bindingMode, callbackAction);
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
      /// When overridden, allows application developers to customize behavior immediately prior to the
      /// <see cref="T:Xamarin.Forms.Page" /> becoming visible.
      /// </summary>
      /// <remarks>To be added.</remarks>
      protected override void OnAppearing()
      {
         base.OnAppearing();

         PageIsAppearing?.Invoke(this);
      }

      /// <summary>
      /// Called when [disappearing].
      /// </summary>
      /// <remarks>To be added.</remarks>
      protected override void OnDisappearing()
      {
         base.OnDisappearing();

         Disappear();
      }

      /// <summary>
      /// Sets up new content.
      /// </summary>
      protected virtual void SetUpNewContent()
      {
         base.Content.SetLifecycleReporters(this);
      }

      /// <summary>
      /// Disappears this instance.
      /// </summary>
      private void Disappear()
      {
         PageIsDisappearing?.Invoke(this);

         this.SendObjectDisappearingMessage();
      }

      /// <summary>
      /// Releases the unmanaged resources.
      /// </summary>
      private void ReleaseUnmanagedResources()
      {
         // TODO release unmanaged resources here
      }
   }
}