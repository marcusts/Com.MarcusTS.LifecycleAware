#region License

// Copyright (c) 2019  Marcus Technical Services, Inc. <marcus@marcusts.com>
//
// This file, AppWithLifecycle.cs, is a part of a program called AccountViewMobile.
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

namespace Com.MarcusTS.LifecycleAware.Views.App
{
   using Common.Interfaces;
   using Common.Utils;
   using SharedUtils.Utils;
   using Xamarin.Forms;

   /// <summary>
   /// Class AppWithLifecycle.
   /// Implements the <see cref="Xamarin.Forms.Application" />
   /// Implements the <see cref="Com.MarcusTS.LifecycleAware.Common.Interfaces.IReportAppLifecycle" />
   /// Implements the <see cref="Com.MarcusTS.LifecycleAware.Common.Interfaces.IManageMainPage" />
   /// </summary>
   /// <seealso cref="Xamarin.Forms.Application" />
   /// <seealso cref="Com.MarcusTS.LifecycleAware.Common.Interfaces.IReportAppLifecycle" />
   /// <seealso cref="Com.MarcusTS.LifecycleAware.Common.Interfaces.IManageMainPage" />
   public class AppWithLifecycle : Application, IReportAppLifecycle, IManageMainPage
   {
      /// <summary>
      /// Gets or sets the root page of the application.
      /// </summary>
      /// <value>The root page of the application.</value>
      /// <remarks>This property throws an exception if the application developer attempts to set it to <see langword="null" />.</remarks>
      public new Page MainPage
      {
         get => base.MainPage;
         set
         {
            base.MainPage = value;

            if (base.MainPage != null)
            {
               base.MainPage.SetLifecycleReporters(this);
            }
         }
      }

      /// <summary>
      /// Occurs when [application is going to sleep].
      /// </summary>
      public event EventUtils.NoParamsDelegate AppIsGoingToSleep;

      /// <summary>
      /// Occurs when [application is resuming].
      /// </summary>
      public event EventUtils.NoParamsDelegate AppIsResuming;

      /// <summary>
      /// Occurs when [application is starting].
      /// </summary>
      public event EventUtils.NoParamsDelegate AppIsStarting;

      /// <summary>
      /// Application developers override this method to perform actions when the application resumes from a sleeping state.
      /// </summary>
      /// <remarks>To be added.</remarks>
      protected override void OnResume()
      {
         AppIsResuming?.Invoke();
      }

      /// <summary>
      /// Application developers override this method to perform actions when the application enters the sleeping state.
      /// </summary>
      /// <remarks>To be added.</remarks>
      protected override void OnSleep()
      {
         AppIsGoingToSleep?.Invoke();
      }

      /// <summary>
      /// Called when [start].
      /// </summary>
      protected override void OnStart()
      {
         AppIsStarting?.Invoke();
      }
   }
}