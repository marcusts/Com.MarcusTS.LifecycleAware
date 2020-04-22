namespace Com.MarcusTS.LifecycleAware.Common.Interfaces
{
   using Com.MarcusTS.SharedUtils.Utils;

   /// <summary>
   ///    Interface IRespondToAppStateChanges
   /// </summary>
   public interface IRespondToAppStateChanges
   {
      /// <summary>
      ///    Occurs when [application is going to sleep].
      /// </summary>
      event EventUtils.NoParamsDelegate AppIsGoingToSleep;

      /// <summary>
      ///    Occurs when [application is resuming].
      /// </summary>
      event EventUtils.NoParamsDelegate AppIsResuming;

      /// <summary>
      ///    Occurs when [application is starting].
      /// </summary>
      event EventUtils.NoParamsDelegate AppIsStarting;
   }
}