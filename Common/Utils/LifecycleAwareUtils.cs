#region License

// Copyright (c) 2019  Marcus Technical Services, Inc. <marcus@marcusts.com>
//
// This file, LifecycleAwareUtils.cs, is a part of a program called AccountViewMobile.
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

namespace Com.MarcusTS.LifecycleAware.Common.Utils
{
   using Interfaces;
   using SharedForms.Common.Notifications;
   using SharedUtils.Events;
   using SharedUtils.Utils;
   using Xamarin.Forms;

   /// <summary>
   /// Class LifecycleAwareUtils.
   /// Implements the <see cref="object" />
   /// </summary>
   /// <seealso cref="object" />
   public static class LifecycleAwareUtils
   {
      /// <summary>
      /// Sends the object disappearing message.
      /// </summary>
      /// <param name="obj">The object.</param>
      public static void SendObjectDisappearingMessage(this object obj)
      {
         FormsMessengerUtils.Send(new ObjectDisappearingMessage {Payload = obj});
      }

      /// <summary>
      /// Sets the application lifecycle reporter.
      /// </summary>
      /// <param name="host">The host.</param>
      /// <param name="hostProperty">The host property.</param>
      /// <param name="newReporter">The new reporter.</param>
      public static void SetAppLifecycleReporter
      (
         this IHostAppLifecycleReporter host,
         ref  IReportAppLifecycle       hostProperty,
         IReportAppLifecycle            newReporter
      )
      {
         ErrorUtils.ConsiderArgumentError(host.IsNullOrDefault(),
                                          "Cannot pass a null host to ->" + nameof(SetAppLifecycleReporter) + "<-");

         var refHost = host;

         if (hostProperty != null)
         {
            CustomWeakEventManager.RemoveEventHandler(
               (
                  sender,
                  args
               ) => refHost.OnAppResuming(),
               nameof(hostProperty.AppIsResuming));
            CustomWeakEventManager.RemoveEventHandler(
               (
                  sender,
                  args
               ) => refHost.OnAppStarting(),
               nameof(hostProperty.AppIsStarting));
            CustomWeakEventManager.RemoveEventHandler(
               (
                  sender,
                  args
               ) => refHost.OnAppGoingToSleep(),
               nameof(hostProperty.AppIsGoingToSleep));
         }

         // Set the new app lifecycle reporter
         hostProperty = newReporter;

         CustomWeakEventManager.AddEventHandler(
            (
               sender,
               args
            ) => refHost.OnAppResuming(),
            nameof(hostProperty.AppIsResuming));
         CustomWeakEventManager.AddEventHandler(
            (
               sender,
               args
            ) => refHost.OnAppStarting(),
            nameof(hostProperty.AppIsStarting));
         CustomWeakEventManager.AddEventHandler(
            (
               sender,
               args
            ) => refHost.OnAppGoingToSleep(),
            nameof(hostProperty.AppIsGoingToSleep));
      }

      /// <summary>
      /// Sets the binding context lifecycle reporters.
      /// </summary>
      /// <param name="parent">The parent.</param>
      public static void SetBindingContextLifecycleReporters(this BindableObject parent)
      {
         ErrorUtils.ConsiderArgumentError(parent.IsNullOrDefault(),
                                          "Cannot submit null parent to ->"           +
                                          nameof(SetBindingContextLifecycleReporters) + "<-");

         parent.BindingContextChanged +=
            (
               sender,
               args
            ) =>
            {
               parent.BindingContext?.SetLifecycleReporters(parent);
            };
      }

      /// <summary>
      /// Sets the lifecycle reporters.
      /// </summary>
      /// <param name="content">The content.</param>
      /// <param name="parent">The parent.</param>
      public static void SetLifecycleReporters
      (
         this object content,
         object      parent
      )
      {
         if (content is IHostAppLifecycleReporter contentAsIHostAppLifecycleReporter)
         {
            if (parent is IReportAppLifecycle parentAsIReportAppLifecycle)
            {
               contentAsIHostAppLifecycleReporter.AppLifecycleReporter = parentAsIReportAppLifecycle;
            }
            else if (parent is IHostAppLifecycleReporter parentAsIHostAppLifecycleReporter)
            {
               contentAsIHostAppLifecycleReporter.AppLifecycleReporter =
                  parentAsIHostAppLifecycleReporter.AppLifecycleReporter;
            }
         }

         if (content is IHostPageLifecycleReporter contentAsIHostPageLifecycleReporter)
         {
            if (parent is IReportPageLifecycle parentAsIReportPageLifecycle)
            {
               contentAsIHostPageLifecycleReporter.PageLifecycleReporter = parentAsIReportPageLifecycle;
            }
            else if (parent is IHostPageLifecycleReporter parentAsIHostPageLifecycleReporter)
            {
               contentAsIHostPageLifecycleReporter.PageLifecycleReporter =
                  parentAsIHostPageLifecycleReporter.PageLifecycleReporter;
            }
         }

         if (content is IHostStageLifecycleReporter contentAsIHostStageLifecycleReporter)
         {
            if (parent is IReportStageLifecycle parentAsIReportStageLifecycle)
            {
               contentAsIHostStageLifecycleReporter.StageLifecycleReporter = parentAsIReportStageLifecycle;
            }
            else if (parent is IHostStageLifecycleReporter parentAsIHostStageLifecycleReporter)
            {
               contentAsIHostStageLifecycleReporter.StageLifecycleReporter =
                  parentAsIHostStageLifecycleReporter.StageLifecycleReporter;
            }
         }
      }

      /// <summary>
      /// Sets the page lifecycle reporter.
      /// </summary>
      /// <param name="host">The host.</param>
      /// <param name="hostProperty">The host property.</param>
      /// <param name="newReporter">The new reporter.</param>
      public static void SetPageLifecycleReporter
      (
         this IHostPageLifecycleReporter host,
         ref  IReportPageLifecycle       hostProperty,
         IReportPageLifecycle            newReporter
      )
      {
         ErrorUtils.ConsiderArgumentError(host.IsNullOrDefault(),
                                          "Cannot pass a null host to ->" + nameof(SetPageLifecycleReporter) + "<-");

         var refHost = host;

         if (hostProperty != null)
         {
            CustomWeakEventManager.RemoveEventHandler(
               (
                  sender,
                  args
               ) => refHost.OnPageAppearing(),
               nameof(hostProperty.PageIsAppearing));
            CustomWeakEventManager.RemoveEventHandler(
               (
                  sender,
                  args
               ) => refHost.OnPageDisappearing(),
               nameof(hostProperty.PageIsDisappearing));
         }

         // Set the new Page lifecycle reporter
         hostProperty = newReporter;

         CustomWeakEventManager.AddEventHandler(
            (
               sender,
               args
            ) => refHost.OnPageAppearing(),
            nameof(hostProperty.PageIsAppearing));
         CustomWeakEventManager.AddEventHandler(
            (
               sender,
               args
            ) => refHost.OnPageDisappearing(),
            nameof(hostProperty.PageIsDisappearing));
      }

      /// <summary>
      /// Sets the stage lifecycle reporter.
      /// </summary>
      /// <param name="host">The host.</param>
      /// <param name="hostProperty">The host property.</param>
      /// <param name="newReporter">The new reporter.</param>
      public static void SetStageLifecycleReporter
      (
         this IHostStageLifecycleReporter host,
         ref  IReportStageLifecycle       hostProperty,
         IReportStageLifecycle            newReporter
      )
      {
         ErrorUtils.ConsiderArgumentError(host.IsNullOrDefault(),
                                          "Cannot pass a null host to ->" + nameof(SetStageLifecycleReporter) + "<-");

         var refHost = host;

         if (hostProperty != null)
         {
            CustomWeakEventManager.RemoveEventHandler(
               (
                  sender,
                  args
               ) => refHost.OnStageAppearing(),
               nameof(hostProperty.StageIsAppearing));
            CustomWeakEventManager.RemoveEventHandler(
               (
                  sender,
                  args
               ) => refHost.OnStageDisappearing(),
               nameof(hostProperty.StageIsDisappearing));
         }

         // Set the new Stage lifecycle reporter
         hostProperty = newReporter;

         CustomWeakEventManager.AddEventHandler(
            (
               sender,
               args
            ) => refHost.OnStageAppearing(),
            nameof(hostProperty.StageIsAppearing));
         CustomWeakEventManager.AddEventHandler(
            (
               sender,
               args
            ) => refHost.OnStageDisappearing(),
            nameof(hostProperty.StageIsDisappearing));
      }
   }
}