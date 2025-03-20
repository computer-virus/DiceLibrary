namespace DiceLibrary {
	/// <summary>
	///		<para>Represents rolling methods for rolling a <see cref="CustomDie"/> instance.</para>
	/// </summary>
	public enum RollMethod {
		/// <summary>
		///		<para>Represents the standard rolling method of rolling once.</para>
		/// </summary>
		Normal,
		
		/// <summary>
		///		<para>Represents the rolling method of rolling twice and keeping the higher result.</para>
		/// </summary>
		Advantage,
		
		/// <summary>
		///		<para>Represents the rolling method of rolling twice and keeping the lower result.</para>
		/// </summary>
		Disadvantage,

		/// <summary>
		///		<para>Represents the exploding method of rolling the die again whenever its highest side is rolled and then adding all the rolls together.</para>
		/// </summary>
		Exploding
	}
}