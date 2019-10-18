Welcome to Drconfused madness!

	Power Series 2 - The Tools of Power

	Auger - Chainsaw - Nailgun - Impact Wrench
				Current version 0.10
				
	
Contents 
* 	WHATS IN THE MOD?
*	TO DO/WISH LIST
*	VERSION HISTORY
*	MADNESS NOTES
*	CREDITS

WHATS IN THE MOD?
	1. Auger and Chainsaw now have have more slots. 8 slots total at tier 6. (This does not update pre-existing items so you have to make or find one new)

	2. Adds in an upgrade to the Nailgun through the recipes called Quicker Fixer Upper

	3.Attachments for the auger and chainsaw 
		-modEngineTurbo1 = 10% increase in speed
		-modEngineTurbo2 = 25% increase in speed
		-modEngineTurbo3 = 50% increase in speed
		
		-modGasTank1 = 25% increase in gas tank size
		-modGasTank2 = 50% increase in gas tank size
		-modGasTank3 = 100% increase in gas tank size
		
		-modReserveGasTank1 = 25% increase in gas tank size
		-modReserveGasTank2 = 50% increase in gas tank size
		-modReserveGasTank3 = 100% increase in gas tank size
		
		-modMotorToolExtension1 = 10% increase in range
		-modMotorToolExtension2 = 25% increase in range
		-modMotorToolExtension3 = 50% increase in range
		
		-modDiamondTip1 = 10% increased block damage but -100% damage to wood
		-modDiamondTip2 = 25% increased block damage but -100% damage to wood
		-modDIamondTip3 = 50% increased block damage but -100% damage to wood
		
		-modMotorToolSilencer = makes it so you can now mine and chop in peace. 
			
		-modChainsawTemperedFrame1 = 
			25% increase in degradation max and lower degradation per use
		-modChainsawTemperedFrame2 = 
			50% increase in degradation max and lower degradation per use
		-modChainsawTemperedFrame3 = 
			75% increase in degradation max and lower degradation per use

	4. All added into the recipes, progression tables and loot tables.
	
	5. Adds in Elucidus Impact Wrench 

			
			
TO DO/WISH LIST
	1. Work towards balance - fun, and challenge
	2. Changing recipes to reflect their nature to make it more immersive
	3. Test the car battery in items.xml effect_group ModSlots to see if the batteries can be affected by a mod_item and if that can carry into the battery bank
	4. Discover if a wider auger can be built and how those attributes work
	5. Carbon Alloy Framing for the motortools to increase durability related values
	6. Figure out how the tools affect the heatmap and if it can be separated from the sound values or if those are intrinsically linked. If it is separate and there is a heat specific value than a heat sink could be developed to soak up the heat rather than the Noise Dampner doing both.
			this is inside of a torch item
			<property name="HeatMapStrength" value="4.05"/>
			<property name="HeatMapTime" value="1200"/>
	7. Considering making a Reserve Fuel Tank attachment that would allow for a 2nd fuel tank attachment for even more fuel. 
	8. Schematic icons built and schematics activated
	9. Recipes built/balanced and added into the progression tables. Considering making unique recipe requirements so that engineering & science are both needed or finding loot to craft these items.
	10. loot.xml additions so the added items can be distributed.
	11. Nail gun additions
	12. Impact Wrench additions if I can get permission from Elucidus.
	13. Get a lawnmower icon in and see if something in hold position could be good candidate for a future model addition of a lawnmower.
	14. Make an sdx version and add a localization for better in game look.
	15. See if a item_modification can increase harvestcount from harvesting blocks, almost like an added motherlode attribute.
			<passive_effect name="HarvestCount" operation="perc_add" value=".2,1" level="1,5" tags="oreWoodHarvest"/>
	
VERSION HISTORY

		
	0.10
		-Name change 
		-Added in Elucidus mod Impact Wrench
		-Added extra slots to the Auger and Chainsaw 
		
	0.09
		-Adapting corrections outlined by RussianDood
			-corrected loot.xml 
				-had modMelee1 when it should have been modMeleeT1
					-same modMelee2 and 3
				-now there should be more occurrences of the mods appearing in loot.
			-added in items.xml
				-description keys for the schematics
			-RussianDood compiled a localization file for the mod.
		-in loot.xml
			adjusted economic values so there is more consistency
				#1Schematics value 250
				#2Schematics value 500
				#3Schematics value 750
	0.08
		-made the 6th slot open up for a tier 6 chainsaw or auger
		-made the blade extension and diamondtip blade able to be slotted at the same time.
		-added items into loot tables
		-activated schematics in recipes
		-Started defining out resourceRefinedSilicon in the xml with future plans to add in new resources for the Power Series
		-Added in an upgrade for the NailGun called the Quicker Fixer Upper, which increases the build speed of the NailGun (if you use any of the nail gun speed enhancements this one might be redundant)
		
	0.07
		-Added schematic icons for all of the current icons in the mod.
		-adapted some feedback by RussianDood after he test my mod out
			corrected progression file to properly add them into progression.
			corrected a case sensitive error in sounds.xml
		
	0.06
		-added in Reserve Gas Tank with icon and tints that can stack with the other Gas Tank for those that prefer more gas over the other attachments. (Currently keeping both using a percentage increase rather than hard number, but may change that)
		-added tint to the customicon in diamond blades
		-adding the progression file and putting the recipes in to be bound to the gating system.
		-added tint to the gas tank 2, 3
		-added Chainsaw Tempered Frame mod (be careful this is a permanent attachment to your chainsaw and you can't change it out.
		
		
	0.05
		adding in the motor tool silencer with its own icon.

	0.04
		adding in icons for	
			-Engine Turbo mods
			-Gas Tank mods
			-Better Handles 

	0.03
		adding in Better Handles to reduce kickback/recoil
		added in an icon for the augerblade mods (*my original icon feel free to use it as you wish)

		
	0.02
		adding in 2 other version of each mod tool.
		
		-modEngineTurbo1 = 10% increase in speed
		-modEngineTurbo2 = 25% increase in speed
		-modEngineTurbo3 = 50% increase in speed
		
		-modGasTank1 = 25% increase in gas tank size
		-modGasTank2 = 50% increase in gas tank size
		-modGasTank3 = 100% increase in gas tank size
		
		-modMotorToolExtension1 = 10% increase in range
		-modMotorToolExtension2 = 25% increase in range
		-modMotorToolExtension3 = 50% increase in range
		
		-modDiamondTip1 = 10% increased block damage but -100% damage to wood
		-modDiamondTip2 = 25% increased block damage but -100% damage to wood
		-modDIamondTip3 = 50% increased block damage but -100% damage to wood		
	
	0.01
		 Initial build. Established the direction the mod is to take. Simply put, to add attachments to the motor tools/power tools in the game so that immersion with increasing the function of the powertools is less game breaking than just making the tool overall better right from the start.
		 
		 - modEngineTurbo1 - a 40% increase to engine speed when installed into either the chainsaw or auger.
		 - modGasTank1 - a 50% increase in gas capacity when installed into either the chainsaw or auger.
		 - modMotorToolExtension - a 50% increase in range when installed into either the chainsaw or auger.
		 


		

MADNESS NOTES

	-HANDLING VALUES = 
		I spent a bunch of time testing some of the different HANDLING VALUES only to realize that I was running another mod that interacted with those values and it has a 'z' on the start of its name so it was overriding all my tests. Going to have to get into a habit of using exportcurrentconfigs.
		At this time my better handles are going to stay as is, till I grasp better how to balance it with the other attachments, especially if I add in things like the extension causing handling issues.
		I made one of the testing attachments have the same HANDLING values as the rocket launcher and wow it was damn hard to control.
		
	-SORT ORDER = 
		I believe the order of loading is done alphabetically. Makes me kind of wish there was a LOOT (skyrim) type sorter or a load order program for running our mods.
	
	-ITEM_MODIFIERS 
		TAGS =
			installable_tags
				this checks the item when attempting to place the mod into the slot to see if it has the tag if yes than does it currently have something in is modifier_tags already? if no allow insert.
			modifier_tags
				this mainly references the location and is in place to prevent doubling up on modifiers when we only want one modifier to be capable of being added in that slot.
			blocked_tags
				possibly an item has both the installable and modifier tag but is blocked from being able to use this modifier.
			type
				attachment
					can be removed from the slot
				mod
					cannot be removed from the slot 
			rarity
				I believe this is referenced by the loot ?
		PROGRESSION.XML
			Learned that for an items recipe to be added into the progression tables than it needs to have the code  tags="learnable" in its recipe name= line.
			If you put a recipe into 2 different perks you only need one of those perks meeting the requirement to unlock the recipe. I think the way around that would be to make the recipe require 2 or more items that can only be crafted by the specific perks, this way that recipe in theory requires the extra perks for it to be crafted. Or it has to be a team effort, or if the items are in the loot tables then it has to be found.
	
	
CREDITS

	Impact Wrench - Elucidus gave permission to use and modify as I see fit. It is very appreciated!! 
		https://www.nexusmods.com/7daystodie/mods/173
		
	




