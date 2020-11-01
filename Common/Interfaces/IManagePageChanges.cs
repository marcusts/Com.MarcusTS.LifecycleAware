namespace Com.MarcusTS.LifecycleAware.Common.Interfaces
{
   using Xamarin.Forms;

   /// <summary>
   ///    Interface IManagePageChanges
   /// </summary>
   public interface IManagePageChanges
   {
      /// <summary>
      ///    Sets the main page.
      /// </summary>
      /// <param name="newPage">The new page.</param>
      void SetMainPage(Page newPage);
   }
}
