namespace Com.MarcusTS.LifecycleAware.Common.Interfaces
{
   using Com.MarcusTS.SharedUtils.Utils;

   /// <summary>
   ///    Interface IReportPageLifecycle
   /// </summary>
   public interface IReportPageLifecycle
   {
      /// <summary>
      ///    Occurs when [page is appearing].
      /// </summary>
      event EventUtils.GenericDelegate<object> PageIsAppearing;

      /// <summary>
      ///    Occurs when [page is disappearing].
      /// </summary>
      event EventUtils.GenericDelegate<object> PageIsDisappearing;
   }
}