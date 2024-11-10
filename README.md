# DiceLibrary
A small library consisting of a couple of classes for the handling dice and dice rolling.

## Available Classes
- Dice: A list of of the Die class with extra functionality
- (abstract) Die: A rollable die
- D (derived from Die): A rollable die with the standard arrangement of faces
- CustomDie (derived from Die): A rollable die with a custom arrangement of faces

## Code Example
```cs
using DiceLibrary;

namespace Lv5AssassinRogueDemo {
	internal class Program {
		static void Main(string[] args) {
			bool isSurprising = new Random().Next(2) == 1; // 50% Chance To Be Surprising
			bool ally5ft = new Random().Next(2) == 1; // 50% Chance To Have Ally Within 5ft Of Target

			int proficiencyBonus = 3;
			int modifier = 5;

			Die d20 = new D(20);

			Dice weaponDamage = [new D(8)];
			Dice sneakAttackDamage = [];
			sneakAttackDamage.Add(3, 6);

			int toHit;
			if (isSurprising) {
				toHit = d20.Advantage() + modifier + proficiencyBonus;
			} else {
				toHit = d20.Roll() + modifier + proficiencyBonus;
			}

			bool crit = isSurprising || toHit == d20.Faces.Max() + modifier + proficiencyBonus;
			if (crit) {
				weaponDamage.Add(weaponDamage.Count, weaponDamage[0].Size);
				sneakAttackDamage.Add(sneakAttackDamage.Count, sneakAttackDamage[0].Size);
			}

			int damage = weaponDamage.Roll().Sum() + modifier;
			if (isSurprising || ally5ft) {
				damage += sneakAttackDamage.Roll().Sum();
			}

			Console.WriteLine($"Surprising Enemy: {isSurprising}");
			Console.WriteLine($"Ally Within 5ft Of Enemy: {ally5ft}");
			Console.WriteLine($"To-Hit Roll: {toHit}");
			Console.WriteLine($"Critical Hit: {crit}");
			Console.WriteLine($"Damage Roll: {damage}");
		}
	}
}
```