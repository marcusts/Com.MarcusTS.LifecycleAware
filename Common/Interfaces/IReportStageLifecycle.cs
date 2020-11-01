namespace Com.MarcusTS.LifecycleAware.Common.Interfaces
{
   using Com.MarcusTS.SharedUtils.Utils;

   /// <summary>
   ///    Interface IReportStageLifecycle
   /// </summary>
   public interface IReportStageLifecycle
   {
      /// <summary>
      ///    Occurs when [stage is appearing].
      /// </summary>
      event EventUtils.GenericDelegate<object> StageIsAppearing;

      /// <summary>
      ///    Occurs when [stage is disappearing].
      /// </summary>
      event EventUtils.GenericDelegate<object> StageIsDisappearing;

      /// <summary>
      ///    Raises the is appearing.
      /// </summary>
      void RaiseIsAppearing();

      /// <summary>
      ///    Raises the is disappearing.
      /// </summary>
      void RaiseIsDisappearing();
   }
}
