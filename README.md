# DiceLibrary
A small, easy-to-use library consisting of a couple of classes for the handling of dice and dice rolling.

## Class Documentation
### `GenericDie<T>`
A generic representation of a rollable die with a custom arrangement of type `T` faces.
#### Properties
- `Faces`: A `List<T>` representing the faces of the die.
- `Size`: An `int` representing the number of faces on the die.
- `Seed`: An`int?` used to determine the die's random rolls.
#### Methods
- `virtual T Roll()`: Rolls the die, returning the result.
- `virtual List<T> Roll(int n)`: Rolls the die `n` times, returning the result.
- `static GenericDie<T> Parse(string s)`: Converts `s` into a die.
- `string ToString()`: Converts the die into a `string`.

### `CustomDie` (derives from `GenericDie<int>`)
A representation of a rollable die with a custom arrangement of type `int` faces.
All properties and methods available to `GenericDie<int>` are also available to `CustomDie`.
#### Additional Properties
- `Max`: An `int` representation of the highest value of the faces on the die.
- `Min`: An `int` representation of the lowest value of the faces on the die.
- `Average`: A `double` representation of the lowest value of the faces on the die.
#### Additional Methods
- `virtual int Roll(RollMethod method)`: Rolls the die using the specified `method`, returning the result.
- `virtual List<int> Roll(int n, RollMethod method)`: Rolls the die `n` times, using the specified `method`, returning the result.
- `virtual bool DC(int dc, RollMethod method, int modifier. bool crits)`: Rolls the die using the specified `method`, adds the specified `modifier`, and then returns whether or not the result is greater than or equal to the specified `dc`. Whether a die can automatically succeed or fail based on rolling its highest or lowest side, respectively, is determined by `crits`. Only the `dc` and `modifier` parameters are mandatory as the method has been overloaded. By default, `method` is `RollMethod.Normal` and `crits` is false.

### `Die` (derives from `CustomDie`)
A representation of a rollable die with the standard arrangement of type `int` faces.
All properties and methods available to `CustomDie` are also available to `Die`.

## Class List Documentation
### `GenericDice<T>` (derives from `List<GenericDie<T>>`)
A `List<GenericDie<T>>` with additional functionality.
All properties and methods available to `List<GenericDie<T>>` are also available to `GenericDice<T>`.
#### Additional Methods
- `virtual List<T> Roll()`: Rolls all of the dice in the list, returning the result.
- `virtual List<T> Roll(int n)`: Rolls all of the dice in the list `n` times, returning the result.
- `static GenericDice<T> Parse(string s)`: Converts `s` into a list of dice.
- `string ToString()`: Converts the list of dice into a `string`.

### `Dice` (derives from `List<CustomDie>`)'
A `List<CustomDie>` with additional functionality.
All properties and methods available to `GenericDice<int>` are also available to `Dice`.
#### Additional Methods
- `virtual List<int> Roll(RollMethod method)`: Rolls all of the dice in the list using the specified `method`, returning the result.
- `virtual List<int> Roll(int n, RollMethod method)`: Rolls all of the dice in the list `n` times, using the specified `method`, returning the result. 

## Enum Documentation
### `RollMethod` (`Enum`)
Represents rolling methods for rolling a die.
#### Values
- `RollMethod.Normal`: Represents the standard rolling method.
- `RollMethod.Advantage`: Represents the rolling method of rolling twice and keeping the higher result.
- `RollMethod.Disadvantage`: Represents the rolling method of rolling twice and keeping the lower result.
- `RollMethod.Exploding`: Represents the rolling method of rolling the die again whenever its highest side is rolled and then adding all the rolls together.

## Console App Example
```cs
using DiceLibrary;

namespace DiceDemo {
	internal class Program {
		static void Main(string[] args) {
			// Standard d20
			Die d20 = new(20);

			// Character stats (Lv 2 Barbarian)
			int profBonus = 2;
			int abilityMod = 3;

			// Character Rage
			RollMethod reckless = RollMethod.Advantage;
			int rageDMG = 2;

			// Character Weapon (2d6)
			Dice greatSword = [new Die(6), new Die(6)];

			// Monster Stats
			int hp = 24;
			int ac = 13;

			// To Hit Calculations
			int roll = d20.Roll(reckless);
			bool crit = false;
			
			if (roll == d20.Max) {
				crit = true;			
			}

			int toHit = roll + abilityMod + profBonus;

			Console.WriteLine($"To Hit: ({roll}) + {abilityMod} + {profBonus} = {toHit}{(crit ? " (Crit!)" : string.Empty)}");

			// Hit Calculations
			int damageRoll;
			int damage;

			if (crit) {
				damageRoll = greatSword.Roll(2).Sum();
				damage = damageRoll + abilityMod + rageDMG;

				Console.WriteLine("The monster took a crit!");
			} else if (toHit >= ac) {
				damageRoll = greatSword.Roll().Sum();
				damage = damageRoll + abilityMod + rageDMG;

				Console.WriteLine("The monster was hit.");
			} else {
				damageRoll = 0;
				damage = 0;

				Console.WriteLine("The monster avoided the attack...");
			}

			if (damage != 0) {
				int newHP = hp - damage;
				Console.WriteLine($"Damage: ({damageRoll}) + {abilityMod} + {rageDMG} = {damage}");
				Console.WriteLine($"HP: {hp} - {damage} = {newHP}");
			}
		}
	}
}
```