// *********************************************************************************
// <copyright file=LifecycleAwareUtils.cs company="Marcus Technical Services, Inc.">
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

namespace Com.MarcusTS.LifecycleAware.Common.Utils
{
   using Com.MarcusTS.LifecycleAware.Common.Interfaces;
   using Com.MarcusTS.SharedForms.Common.Notifications;
   using Com.MarcusTS.SharedUtils.Events;
   using System.Diagnostics;
   using Xamarin.Forms;

   /// <summary>
   ///    Class LifecycleAwareUtils.
   ///    Implements the <see cref="object" />
   /// </summary>
   /// <seealso cref="object" />
   public static class LifecycleAwareUtils
   {
      /// <summary>
      ///    Sends the object disappearing message.
      /// </summary>
      /// <param name="obj">The object.</param>
      public static void SendObjectDisappearingMessage(this object obj)
      {
         FormsMessengerUtils.Send(new ObjectDisappearingMessage { Payload = obj });
      }

      /// <summary>
      ///    Sets the application lifecycle reporter.
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
         Debug.Assert(host != null, "Cannot pass a null host to ->" + nameof(SetAppLifecycleReporter) + "<-");

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
      ///    Sets the binding context lifecycle reporters.
      /// </summary>
      /// <param name="parent">The parent.</param>
      public static void SetBindingContextLifecycleReporters(this BindableObject parent)
      {
         Debug.Assert(parent != null,
                      "Cannot submit null parent to ->" + nameof(SetBindingContextLifecycleReporters) + "<-");

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
      ///    Sets the lifecycle reporters.
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
      ///    Sets the page lifecycle reporter.
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
         Debug.Assert(host != null, "Cannot pass a null host to ->" + nameof(SetPageLifecycleReporter) + "<-");

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
      ///    Sets the stage lifecycle reporter.
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
         Debug.Assert(host != null, "Cannot pass a null host to ->" + nameof(SetStageLifecycleReporter) + "<-");

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