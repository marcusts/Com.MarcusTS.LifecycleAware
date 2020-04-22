namespace Com.MarcusTS.LifecycleAware.Common.Interfaces
{
   /// <summary>
   ///    Interface ICleanUpBeforeFinalization
   /// </summary>
   public interface ICleanUpBeforeFinalization
   {
      /// <summary>
      ///    Gets or sets a value indicating whether this instance is cleaning up before finalization.
      /// </summary>
      /// <value><c>true</c> if this instance is cleaning up before finalization; otherwise, <c>false</c>.</value>
      bool IsCleaningUpBeforeFinalization { get; set; }
   }
}