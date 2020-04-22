namespace Com.MarcusTS.SmartDI.LifecycleAware
{
   using Com.MarcusTS.SharedForms.Common.Notifications;

   /// <summary>
   ///    Interface ISmartDIContainerWithLifecycle
   /// </summary>
   public interface ISmartDIContainerWithLifecycle
   {
   }

   /// <summary>
   ///    Class SmartDIContainerWithLifecycle.
   ///    Implements the <see cref="Com.MarcusTS.SmartDI.SmartDIContainer" />
   ///    Implements the <see cref="Com.MarcusTS.SmartDI.LifecycleAware.ISmartDIContainerWithLifecycle" />
   /// </summary>
   /// <seealso cref="Com.MarcusTS.SmartDI.SmartDIContainer" />
   /// <seealso cref="Com.MarcusTS.SmartDI.LifecycleAware.ISmartDIContainerWithLifecycle" />
   public class SmartDIContainerWithLifecycle : SmartDIContainer, ISmartDIContainerWithLifecycle
   {
      /// <summary>
      ///    Initializes a new instance of the <see cref="SmartDIContainerWithLifecycle" /> class.
      /// </summary>
      public SmartDIContainerWithLifecycle()
      {
         FormsMessengerUtils.Subscribe<ObjectDisappearingMessage>(this, OnObjectDisappearing);
      }

      /// <summary>
      ///    Called when [object disappearing].
      /// </summary>
      /// <param name="sender">The sender.</param>
      /// <param name="mess">The mess.</param>
      private void OnObjectDisappearing
      (
         object                    sender,
         ObjectDisappearingMessage mess
      )
      {
         ContainerObjectIsDisappearing(mess.Payload);
      }
   }
}