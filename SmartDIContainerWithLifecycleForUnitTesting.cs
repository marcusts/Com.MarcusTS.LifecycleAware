namespace Com.MarcusTS.SmartDI.LifecycleAware
{
   using Com.MarcusTS.SharedForms.Common.Notifications;

   /// <summary>
   ///    Interface ISmartDIContainerWithLifecycleForUnitTesting
   ///    Implements the <see cref="Com.MarcusTS.SmartDI.ISmartDIContainerForUnitTesting" />
   /// </summary>
   /// <seealso cref="Com.MarcusTS.SmartDI.ISmartDIContainerForUnitTesting" />
   public interface ISmartDIContainerWithLifecycleForUnitTesting : ISmartDIContainerForUnitTesting
   {
   }

   /// <summary>
   ///    Class SmartDIContainerWithLifecycleForUnitTesting.
   ///    Implements the <see cref="Com.MarcusTS.SmartDI.SmartDIContainerForUnitTesting" />
   ///    Implements the <see cref="Com.MarcusTS.SmartDI.LifecycleAware.ISmartDIContainerWithLifecycleForUnitTesting" />
   /// </summary>
   /// <seealso cref="Com.MarcusTS.SmartDI.SmartDIContainerForUnitTesting" />
   /// <seealso cref="Com.MarcusTS.SmartDI.LifecycleAware.ISmartDIContainerWithLifecycleForUnitTesting" />
   public class SmartDIContainerWithLifecycleForUnitTesting : SmartDIContainerForUnitTesting,
                                                              ISmartDIContainerWithLifecycleForUnitTesting
   {
      /// <summary>
      ///    Initializes a new instance of the <see cref="SmartDIContainerWithLifecycleForUnitTesting" /> class.
      /// </summary>
      public SmartDIContainerWithLifecycleForUnitTesting()
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