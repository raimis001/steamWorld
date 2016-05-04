using System;
using System.Collections.Generic;
using UnityEngine;

//public enum ItemType { RESOURCE, MATERIAL, BUILD, TOOL, ITEM, PLANT }
public enum ItemTypes {NONE, RESOURCE, DRINK, EAT, ENERGY }
public enum ActionTypes { NONE, INVENTORY, WORKBENCH, VAGA, BLOCK, DOOR }
public enum RecepieTypes { WORK, NONSTOP }
public enum InstrumentTypes { HAMMER, HOE, AXE, PICKAXE, UNIVERSAL }

public class TypeClass {
	public ItemTypes Type = ItemTypes.NONE;
	public int Amount = 0;

	public TypeClass(ItemTypes type, int amount) {
		Type = type;
		Amount = amount;
	}
}

public class InstrumentClassTEMP {
	public InstrumentTypes Type;
	public List<ItemNames> Buildings = new List<ItemNames>();

	public InstrumentClassTEMP(InstrumentTypes type) {
		Type = type;
	}
}

#region ITEMS
	public class ItemBase {
		private Sprite _sprite;
		private GameObject _prefab;

		public ItemNames id;
		public ActionTypes Action = ActionTypes.NONE;
		public string Name;
		public string Description;
		public string IconName;
		public string PrefabName;

		public Sprite Icon {
			get {
				if (!_sprite) {
					_sprite = Resources.Load<Sprite>(IconName);
				}
				return _sprite;
			}
		}
		public GameObject Prefab {
			get {
				if (!_prefab) {
					_prefab = Resources.Load<GameObject>(PrefabName);
				}

				return _prefab;
			}
		}

		public virtual ItemClass Item {
			get {
				return new ItemClass(id, 1);
			}
		}
	}

	public class ItemResource : ItemBase {
		public Dictionary<ItemTypes, TypeClass> Actions = new Dictionary<ItemTypes, TypeClass>();

		public void AddAction(ItemTypes type, int amount) {
			Actions.Add(type, new TypeClass(type, amount));
		}

	public int HaveAction(ItemTypes type) {

		TypeClass cls = null;
		Actions.TryGetValue(type, out cls);

		if (cls != null)
			return cls.Amount;

		return -1;
		
	}

	}

	public class ItemBuilding : ItemBase {
		public List<int> Fuel = new List<int>();
	}

	public class ItemInstrument : ItemBase {
		public InstrumentTypes InstrumentType;
	}

public class ItemDraft : ItemBase {
	public ItemNames Result;

	public List<ItemClass> Required = new List<ItemClass>();
	public void AddRequired(ItemNames itemID, int amount) {
		ItemClass item = new ItemClass(itemID, 0, amount);
		Required.Add(item);
	}
}

public class ItemRecepie : ItemBase {
	public ItemNames AffiliateID;
	public float Works = 10;
	public RecepieTypes RecepieType = RecepieTypes.WORK; 

	public List<ItemClass> Required = new List<ItemClass>();
	public List<ItemClass> Result = new List<ItemClass>();

	public void AddRequired(ItemNames itemID, int amount) {
		ItemClass item = new ItemClass(itemID, 0, amount);
		Required.Add(item);
	}
	public void AddResult(ItemNames itemID, int amount) {
		Result.Add(new ItemClass(itemID, amount));
	}
}
public class ItemGrain : ItemBase {
	public ItemNames Plant;
}

public class ItemPlant : ItemBase {
	public float RipeTime = 10;
	public float GrowTime = 0;

}

public class ItemAnimal : ItemBase {

}

public class ItemWeapon : ItemBase {
	public List<ItemNames> Ammo = new List<ItemNames>();
}

public class ItemAmmo : ItemBase {

}

public class ItemFuel : ItemBase {
	public ItemNames FuelID;
	public float FuelTime;

	public override ItemClass Item {
		get {
			return new ItemClass(FuelID, 1);
		}
	}

}


#endregion ITEMS

#region ItemEnum
public enum ItemNames {
	all,
	wood, stone, copperOre, ironOre, straw, water, skin, flax, wheat, artichok, goji, meat, 
	coal, planks, stoneBrick, stoneShard, flaxFiber, copper, iron, bendable, 
	workbench, chest, well, field, stoneMill, campfire, wallBlock, door, spring, 
	hammer, hoe, axe, pickaxe, 
	grainWheat, 
	plantWheat, plantArtichok, plantFlax, plantGoji, 
	weaponCrossbow, 
	ammoArrow,
	bear, 
	recepieBendable, recepiePlanks, recepieBricks, recepieShard, recepieFlaxFiber, recepieCopper, recepieIron, recepieWater, recepieHammer, recepieHoe, recepieGrainWheat, recepieSpringWater,
	draftWorkbench,  
	fuelWood,
	
}
#endregion

public class ItemsDB {
	public static Dictionary<ItemNames, ItemBase> Items = new Dictionary<ItemNames, ItemBase>();
	public static Dictionary<InstrumentTypes, InstrumentClassTEMP> Instruments = new Dictionary<InstrumentTypes, InstrumentClassTEMP>();

	public static void Init() {


		#region RESURSI
		ItemResource item;

		item = new ItemResource();
		item.id = ItemNames.wood;
		item.AddAction(ItemTypes.RESOURCE, 1);
		item.Name = "Koksne";
		item.Description = "Tiek izmantota dažādu kokmateriālu iegūšanai";
		item.IconName = "Icons/lumber_wood";
		item.PrefabName = "Loots/lootWood";
		Items.Add(item.id, item);

		item = new ItemResource();
		item.id = ItemNames.stone;//2
		item.AddAction(ItemTypes.RESOURCE, 1);
		item.Name = "Akmens";
		item.Description = "Izmanto būvniecībā un akmens izstrādājumu iegūšani";
		item.IconName = "Icons/stone";
		item.PrefabName = "Loots/lootStone";
		Items.Add(item.id, item);

		item = new ItemResource();
		item.id = ItemNames.copperOre;//3
		item.AddAction(ItemTypes.RESOURCE, 1);
		item.Name = "Vara rūda";
		item.Description = "No rūdas iegūst varu";
		item.IconName = "Icons/copperOre";
		item.PrefabName = "Loots/lootCopperOre";
		Items.Add(item.id, item);

		item = new ItemResource();
		item.id = ItemNames.ironOre;//4
		item.AddAction(ItemTypes.RESOURCE, 1);
		item.Name = "Dzelzs rūda";
		item.Description = "No rūdas iegūst dzelzi";
		item.IconName = "Icons/ironOre";
		item.PrefabName = "Loots/lootIronOre";
		Items.Add(item.id, item);

		item = new ItemResource();
		item.id = ItemNames.straw;//5
		item.AddAction(ItemTypes.RESOURCE, 1);
		item.Name = "Salmi";
		item.Description = "Tos iegūst no labības un izmanto primitīvu guļvietu būvniecībā.";
		item.IconName = "Icons/straw";
		item.PrefabName = "Loots/lootStraw";
		Items.Add(item.id, item);

		item = new ItemResource();
		item.id = ItemNames.water;//6
		item.AddAction(ItemTypes.RESOURCE, 1);
		item.AddAction(ItemTypes.DRINK, 1);
		item.Name = "Ūdens";
		item.Description = "Visa dzīvā sākums.";
		item.IconName = "Icons/water";
		item.PrefabName = "Loots/lootWater";
		Items.Add(item.id, item);

		item = new ItemResource();
		item.id = ItemNames.skin;//7
		item.AddAction(ItemTypes.RESOURCE, 1);
		item.Name = "Āda";
		item.Description = "Noderīga lieta.";
		item.IconName = "Icons/hide";
		item.PrefabName = "Loots/lootHide";
		Items.Add(item.id, item);

		item = new ItemResource();
		item.id = ItemNames.flax;//8
		item.AddAction(ItemTypes.RESOURCE, 1);
		item.Name = "Lini";
		item.Description = "Noderīga lieta.";
		item.IconName = "Icons/flax";
		item.PrefabName = "Loots/lootFlax";
		Items.Add(item.id, item);

		item = new ItemResource();
		item.id = ItemNames.wheat;//9
		item.AddAction(ItemTypes.RESOURCE, 1);
		item.Name = "Kvieši";
		item.Description = "Noderīga lieta.";
		item.IconName = "Icons/wheat";
		item.PrefabName = "Loots/lootWheat";
		Items.Add(item.id, item);

		item = new ItemResource();
		item.id = ItemNames.artichok;//10
		item.AddAction(ItemTypes.RESOURCE, 1);
		item.Name = "Topinabūrs";
		item.Description = "Noderīga lieta.";
		item.IconName = "Icons/ginger";
		item.PrefabName = "Loots/lootGinger";
		Items.Add(item.id, item);

		item = new ItemResource();
		item.id = ItemNames.goji;//11
		item.AddAction(ItemTypes.RESOURCE, 1);
		item.Name = "Goji ogas";
		item.Description = "Noderīga lieta.";
		item.IconName = "Icons/goji";
		item.PrefabName = "Loots/lootGoji";
		Items.Add(item.id, item);

		item = new ItemResource();
		item.id = ItemNames.meat;//12
		item.AddAction(ItemTypes.RESOURCE, 1);
		item.Name = "Meat";
		item.Description = "Noderīga lieta.";
		item.IconName = "Icons/meat";
		item.PrefabName = "Loots/lootMeat";
		Items.Add(item.id, item);

		#endregion

		#region MATERIĀLI

		item = new ItemResource();
		item.id = ItemNames.coal;//101
		item.AddAction(ItemTypes.RESOURCE, 1);
		item.Name = "Ogles";
		item.Description = "Noderīga lieta.";
		item.IconName = "Icons/coal";
		item.PrefabName = "Loots/lootCoal";
		Items.Add(item.id, item);


		item = new ItemResource();
		item.id = ItemNames.planks;//102
		item.AddAction(ItemTypes.RESOURCE, 1);
		item.Name = "Dēļi";
		item.Description = "Noderīga lieta.";
		item.IconName = "Icons/planks";
		item.PrefabName = "Loots/lootPlanks";
		Items.Add(item.id, item);

		item = new ItemResource();
		item.id = ItemNames.stoneBrick;//103
		item.AddAction(ItemTypes.RESOURCE, 1);
		item.Name = "Akmens ķieģeļi";
		item.Description = "Noderīga lieta.";
		item.IconName = "Icons/stoneBrick";
		item.PrefabName = "Loots/lootStoneBrick";
		Items.Add(item.id, item);

		item = new ItemResource();
		item.id = ItemNames.stoneShard;//104
		item.AddAction(ItemTypes.RESOURCE, 1);
		item.Name = "Akmens lauskas";
		item.Description = "Noderīga lieta.";
		item.IconName = "Icons/stoneShard";
		item.PrefabName = "Loots/lootStoneShard";
		Items.Add(item.id, item);

		item = new ItemResource();
		item.id = ItemNames.flaxFiber;//105
		item.AddAction(ItemTypes.RESOURCE, 1);
		item.Name = "Šķiedra"; //Flax fiber
		item.Description = "Noderīga lieta.";
		item.IconName = "Icons/flaxFiber";
		item.PrefabName = "Loots/lootFlaxFiber";
		Items.Add(item.id, item);

		item = new ItemResource();
		item.id = ItemNames.copper;//106
		item.AddAction(ItemTypes.RESOURCE, 1);
		item.Name = "Varš"; 
		item.Description = "Noderīga lieta.";
		item.IconName = "Icons/copper";
		item.PrefabName = "Loots/lootCopper";
		Items.Add(item.id, item);

		item = new ItemResource();
		item.id = ItemNames.iron;//107
		item.AddAction(ItemTypes.RESOURCE, 1);
		item.Name = "Dzelzs"; 
		item.Description = "Noderīga lieta.";
		item.IconName = "Icons/iron";
		item.PrefabName = "Loots/lootIron";
		Items.Add(item.id, item);

		item = new ItemResource();
		item.id = ItemNames.bendable;//108
		item.AddAction(ItemTypes.RESOURCE, 1);
		item.Name = "Mērcēts zars";
		item.Description = "Noderīga lieta.";
		item.IconName = "Icons/bendable";
		item.PrefabName = "Loots/lootBendable";
		Items.Add(item.id, item);

		#endregion

		#region BŪVES
		ItemBuilding build;

		build = new ItemBuilding();
		build.id = ItemNames.workbench;//201
		build.Action = ActionTypes.WORKBENCH;
		build.Name = "Darba galds";
		build.Description = "Noderīga lieta.";
		build.IconName = "Icons/workbench";
		build.PrefabName = "Objects/furniture/Workbench";
		Items.Add(build.id, build);

		build = new ItemBuilding();
		build.id = ItemNames.chest;//202
		build.Action = ActionTypes.INVENTORY;
		build.Name = "Lāde";
		build.Description = "Noderīga lieta.";
		build.IconName = "Icons/chest";
		build.PrefabName = "Objects/furniture/Chest";
		Items.Add(build.id, build);

		build = new ItemBuilding();
		build.id = ItemNames.well;//203
		build.Action = ActionTypes.WORKBENCH;
		build.Name = "Aka";
		build.Description = "Noderīga lieta.";
		build.IconName = "Icons/well";
		build.PrefabName = "Objects/outdors/Well";
		Items.Add(build.id, build);

		build = new ItemBuilding();
		build.id = ItemNames.spring;
		build.Action = ActionTypes.WORKBENCH;
		build.Name = "Avots";
		build.Description = "Noderīga lieta.";
		build.IconName = "Icons/spring";
		build.PrefabName = "Objects/outdors/Spring";
		Items.Add(build.id, build);


		build = new ItemBuilding();
		build.id = ItemNames.field;//204
		build.Action = ActionTypes.VAGA;
		build.Name = "Vaga";
		build.Description = "Noderīga lieta.";
		build.IconName = "Icons/farmField";
		build.PrefabName = "Objects/Vaga";
		Items.Add(build.id, build);

		build = new ItemBuilding();
		build.id = ItemNames.stoneMill;//205
		build.Action = ActionTypes.WORKBENCH;
		build.Name = "Dzirnas";
		build.Description = "Noderīga lieta.";
		build.IconName = "Icons/stoneMill";
		build.PrefabName = "Objects/Vaga";
		Items.Add(build.id, build);

		build = new ItemBuilding();
		build.id = ItemNames.campfire;//206
		build.Action = ActionTypes.WORKBENCH;
		build.Name = "Uguskurs";
		build.Description = "Noderīga lieta.";
		build.IconName = "Icons/campfire";
		build.PrefabName = "Objects/outdors/CampFire";
		build.Fuel.Add(1301);
		Items.Add(build.id, build);

		build = new ItemBuilding();
		build.id = ItemNames.wallBlock;//207
		build.Action = ActionTypes.BLOCK;
		build.Name = "Būvkonstrukcija";
		build.Description = "Noderīga lieta.";
		build.IconName = "Icons/wallBlock";
		build.PrefabName = "Objects/WallBlock";
		Items.Add(build.id, build);

		build = new ItemBuilding();
		build.id = ItemNames.door;//208
		build.Action = ActionTypes.DOOR;
		build.Name = "Durvis";
		build.Description = "Noderīga lieta.";
		build.IconName = "Icons/door";
		build.PrefabName = "";
		Items.Add(build.id, build);

		/*TODO iztrūkstošās būves
			parasts uguskurs
			nojume
			vienkāršāku darba galdu, kurā var pagatavot
				āmuru, kas atļauj būvēt
					parastu ugunskuru
					nojume
		*/

		#endregion

		#region INSTRUMENTI
		ItemInstrument instrument;

		InstrumentClassTEMP instr;

		instr = new InstrumentClassTEMP(InstrumentTypes.HAMMER);
		instr.Buildings.Add(ItemNames.draftWorkbench);
		instr.Buildings.Add(ItemNames.wallBlock);
		instr.Buildings.Add(ItemNames.door);
		Instruments.Add(instr.Type, instr);

		instr = new InstrumentClassTEMP(InstrumentTypes.HOE);
		instr.Buildings.Add(ItemNames.field);
		Instruments.Add(instr.Type, instr);

		instrument = new ItemInstrument();
		instrument.id = ItemNames.hammer;//301
		instrument.InstrumentType = InstrumentTypes.HAMMER;
		instrument.Name = "Āmurs";
		instrument.Description = "Noderīga lieta.";
		instrument.IconName = "Icons/toolsHammer";
		instrument.PrefabName = "Loots/lootHammer";
		Items.Add(instrument.id, instrument);

		instrument = new ItemInstrument();
		instrument.id = ItemNames.hoe;//302
		instrument.InstrumentType = InstrumentTypes.HOE;
		instrument.Name = "Kaplis";
		instrument.Description = "Noderīga lieta.";
		instrument.IconName = "Icons/toolsHoe";
		instrument.PrefabName = "Loots/lootHoe";
		Items.Add(instrument.id, instrument);

		instrument = new ItemInstrument();
		instrument.id = ItemNames.axe;//303
		instrument.InstrumentType = InstrumentTypes.AXE;
		instrument.Name = "Cirvis";
		instrument.Description = "Noderīga lieta.";
		instrument.IconName = "Icons/toolsAxe";
		Items.Add(instrument.id, instrument);

		instrument = new ItemInstrument();
		instrument.id = ItemNames.pickaxe;//304
		instrument.InstrumentType = InstrumentTypes.PICKAXE;
		instrument.Name = "Cirtnis";
		instrument.Description = "Noderīga lieta.";
		instrument.IconName = "Icons/toolsPickaxe";
		Items.Add(instrument.id, instrument);

		#endregion

		#region GRAINS
		ItemGrain grain;

		//--------------------------
		grain = new ItemGrain();
		grain.id = ItemNames.grainWheat;//401
		Items.Add(grain.id, grain);
		grain.Name = "Kviešu graudi";
		grain.Description = "Noderīga lieta.";
		grain.IconName = "Icons/grainsWheat";
		grain.PrefabName = "Loots/lootWheatGrain";
		grain.Plant = ItemNames.plantWheat;

		#endregion

		#region PLANTS
		ItemPlant plant;

		//--------------------------
		plant = new ItemPlant();
		plant.id = ItemNames.plantWheat;//501
		Items.Add(plant.id, plant);
		plant.Name = "Kvieši";
		plant.Description = "Noderīga lieta.";
		plant.IconName = "Icons/wheat";
		plant.PrefabName = "Plants/PlantWheat";
		plant.RipeTime = 30;

		//--------------------------
		plant = new ItemPlant();
		plant.id = ItemNames.plantArtichok;//502
		Items.Add(plant.id, plant);
		plant.Name = "Topinabūri";
		plant.Description = "Noderīga lieta.";
		plant.IconName = "Icons/ginger";
		plant.PrefabName = "Plants/PlantArtichok";
		plant.RipeTime = 30;

		//--------------------------
		plant = new ItemPlant();
		plant.id = ItemNames.plantFlax;//503
		Items.Add(plant.id, plant);
		plant.Name = "Lini";
		plant.Description = "Noderīga lieta.";
		plant.IconName = "Icons/flax";
		plant.PrefabName = "Plants/PlantFlax";
		plant.RipeTime = 30;

		//--------------------------
		plant = new ItemPlant();
		plant.id = ItemNames.plantGoji;//504
		Items.Add(plant.id, plant);
		plant.Name = "Goji krūms";
		plant.Description = "Noderīga lieta.";
		plant.IconName = "Icons/goji";
		plant.PrefabName = "Plants/PlantGoji";
		plant.RipeTime = 10;
		plant.GrowTime = 5;

		/*TODO papildināt augus
			koki
			zāles kušķi ar rando dropu
			sēnes
			avots, kas dod nedaudz ūdeni, nekad neizsīkst
		*/
		#endregion

		#region WEAPON 
		ItemWeapon weapon;

		//--------------------------
		weapon = new ItemWeapon();
		weapon.id = ItemNames.weaponCrossbow;//601
		Items.Add(weapon.id, weapon);
		weapon.Name = "Arbalets";
		weapon.Description = "Noderīga lieta.";
		weapon.IconName = "Icons/crossbow";
		weapon.PrefabName = "Objects/weapons/Crossbow";
		weapon.Ammo.Add(ItemNames.ammoArrow);

		#endregion

		#region AMMO
		ItemAmmo ammo;

		//--------------------------
		ammo = new ItemAmmo();
		ammo.id = ItemNames.ammoArrow;//701
		Items.Add(ammo.id, ammo);
		ammo.Name = "Bultas";
		ammo.Description = "Noderīga lieta.";
		ammo.IconName = "Icons/arrow";
		ammo.PrefabName = "Objects/weapons/Arrow";

		#endregion

		#region ANIMALS
		ItemAnimal animal;

		//--------------------------
		animal = new ItemAnimal();
		animal.id = ItemNames.bear;//601
		Items.Add(animal.id, animal);
		animal.Name = "Lācis";
		animal.Description = "Noderīga lieta.";
		animal.IconName = "Icons/crossbow";
		animal.PrefabName = "Plants/PlantWheat";

		#endregion

		#region RECEPTES
		ItemRecepie recepie;

		//--------------------------
		recepie = new ItemRecepie();
		recepie.id = ItemNames.recepieBendable;//1001
		Items.Add(recepie.id, recepie);
		recepie.Name = "Mērcēts zars";
		recepie.Description = "Noderīga lieta.";
		recepie.IconName = "Icons/bendable";
		recepie.AffiliateID = ItemNames.workbench;

		//Result
		recepie.AddResult(ItemNames.bendable, 1);

		//Requred
		recepie.AddRequired(ItemNames.wood, 1);
		recepie.AddRequired(ItemNames.water, 1);


		//--------------------------
		recepie = new ItemRecepie();
		recepie.id = ItemNames.recepiePlanks;//1002
		Items.Add(recepie.id, recepie);
		recepie.Name = "Dēļi";
		recepie.Description = "Noderīga lieta.";
		recepie.IconName = "Icons/planks";
		recepie.AffiliateID = ItemNames.workbench;

		//Result
		recepie.AddResult(ItemNames.planks, 1);

		//Requred
		recepie.AddRequired(ItemNames.wood, 3);

		//--------------------------
		recepie = new ItemRecepie();
		recepie.id = ItemNames.recepieBricks;//1003
		Items.Add(recepie.id, recepie);
		recepie.Name = "Akmens ķieģeļi";
		recepie.Description = "Noderīga lieta.";
		recepie.IconName = "Icons/stoneBrick";
		recepie.AffiliateID = ItemNames.workbench;

		//Result
		recepie.AddResult(ItemNames.stoneBrick, 1);

		//Requred
		recepie.AddRequired(ItemNames.stone, 2);

		//--------------------------
		recepie = new ItemRecepie();
		recepie.id = ItemNames.recepieShard;//1004
		Items.Add(recepie.id, recepie);
		recepie.Name = "Akmens šķembas";
		recepie.Description = "Noderīga lieta.";
		recepie.IconName = "Icons/stoneShard";
		recepie.AffiliateID = ItemNames.workbench;

		//Result
		recepie.AddResult(ItemNames.stoneShard, 5);

		//Requred
		recepie.AddRequired(ItemNames.stone, 1);

		//--------------------------
		recepie = new ItemRecepie();
		recepie.id = ItemNames.recepieFlaxFiber;//1005
		Items.Add(recepie.id, recepie);
		recepie.Name = "Šķiedra";
		recepie.Description = "Noderīga lieta.";
		recepie.IconName = "Icons/flaxFiber";
		recepie.AffiliateID = ItemNames.workbench;

		//Result
		recepie.AddResult(ItemNames.flaxFiber, 5);

		//Requred
		recepie.AddRequired(ItemNames.flax, 2);
		recepie.AddRequired(ItemNames.water, 1);

		//--------------------------
		recepie = new ItemRecepie();
		recepie.id = ItemNames.recepieCopper;//1006
		Items.Add(recepie.id, recepie);
		recepie.Name = "Varš";
		recepie.Description = "Noderīga lieta.";
		recepie.IconName = "Icons/copper";
		recepie.AffiliateID = ItemNames.workbench;

		//Result
		recepie.AddResult(ItemNames.copper, 1);

		//Requred
		recepie.AddRequired(ItemNames.copperOre, 2);

		//--------------------------
		recepie = new ItemRecepie();
		recepie.id = ItemNames.recepieIron;//1007
		Items.Add(recepie.id, recepie);
		recepie.Name = "Dzelzs";
		recepie.Description = "Noderīga lieta.";
		recepie.IconName = "Icons/iron";
		recepie.AffiliateID = ItemNames.workbench;

		//Result
		recepie.AddResult(ItemNames.iron, 1);

		//Requred
		recepie.AddRequired(ItemNames.ironOre, 2);

		//--------------------------
		recepie = new ItemRecepie();
		recepie.id = ItemNames.recepieWater;//1008
		Items.Add(recepie.id, recepie);
		recepie.Name = "Ūdens";
		recepie.Description = "Noderīga lieta.";
		recepie.IconName = "Icons/water";
		recepie.AffiliateID = ItemNames.well;
		recepie.RecepieType = RecepieTypes.NONSTOP;

		//Result
		recepie.AddResult(ItemNames.water, 1);

		//Requred
		//recepie.AddRequired(4, 2);

		//--------------------------
		recepie = new ItemRecepie();
		recepie.id = ItemNames.recepieSpringWater;//1008
		Items.Add(recepie.id, recepie);
		recepie.Name = "Ūdens";
		recepie.Description = "Noderīga lieta.";
		recepie.IconName = "Icons/water";
		recepie.AffiliateID = ItemNames.spring;
		recepie.RecepieType = RecepieTypes.NONSTOP;
		recepie.Works = 100;

		//Result
		recepie.AddResult(ItemNames.water, 1);

		//Requred
		//recepie.AddRequired(4, 2);

		//--------------------------
		recepie = new ItemRecepie();
		recepie.id = ItemNames.recepieHammer;//1009
		Items.Add(recepie.id, recepie);
		recepie.Name = "Akems āmurs";
		recepie.Description = "Noderīga lieta.";
		recepie.IconName = "Icons/toolsHammer";
		recepie.AffiliateID = ItemNames.workbench;
		recepie.RecepieType = RecepieTypes.WORK;

		//Result
		recepie.AddResult(ItemNames.hammer, 1);

		//Requred
		recepie.AddRequired(ItemNames.wood, 1);
		recepie.AddRequired(ItemNames.stoneBrick, 1);

		//--------------------------
		recepie = new ItemRecepie();
		recepie.id = ItemNames.recepieHoe;//1009
		Items.Add(recepie.id, recepie);
		recepie.Name = "Akems kaplis";
		recepie.Description = "Noderīga lieta.";
		recepie.IconName = "Icons/toolsHoe";
		recepie.AffiliateID = ItemNames.workbench;
		recepie.RecepieType = RecepieTypes.WORK;

		//Result
		recepie.AddResult(ItemNames.hoe, 1);

		//Requred
		recepie.AddRequired(ItemNames.wood, 1);
		recepie.AddRequired(ItemNames.stoneBrick, 1);

		//--------------------------
		recepie = new ItemRecepie();
		recepie.id = ItemNames.recepieGrainWheat;//1010
		Items.Add(recepie.id, recepie);
		recepie.Name = "Kviešu graudi";
		recepie.Description = "Noderīga lieta.";
		recepie.IconName = "Icons/grainsWheat";
		recepie.AffiliateID = ItemNames.stoneMill;
		recepie.RecepieType = RecepieTypes.WORK;

		//Result
		recepie.AddResult(ItemNames.grainWheat, 3);

		//Requred
		recepie.AddRequired(ItemNames.wheat, 1);

		/*TODO izveidot ēdamās receptes
			ceptas sēnes
		
		*/

		#endregion

		#region RASĒJUMI
		ItemDraft draft;

		//--------------------------
		draft = new ItemDraft();
		draft.id = ItemNames.draftWorkbench;//1101
		Items.Add(draft.id, draft);
		draft.Name = "Darba galds";
		draft.Description = "Noderīga lieta.";
		draft.IconName = "Icons/workbench";
		draft.PrefabName = "Objects/Site";
		draft.Result = ItemNames.workbench;

		//Requred
		draft.AddRequired(ItemNames.wood, 3);
		draft.AddRequired(ItemNames.stone, 2);

		/*TODO izveidot iztrūkstošos rasējumus
			Galds
			Krēsls
			Lāde
			Aka
			Ugunskurs
			Dzirnavas
			Mājas blokam
		*/

		#endregion

		#region FUELS
		ItemFuel fuel;

		fuel = new ItemFuel();
		fuel.id = ItemNames.fuelWood;//1301
		fuel.Action = ActionTypes.WORKBENCH;
		fuel.Name = "Malka";
		fuel.Description = "Noderīga lieta.";
		fuel.FuelID = ItemNames.wood;
		fuel.FuelTime = 1000;
		Items.Add(fuel.id, fuel);

		#endregion

	}

	public static T GetItem<T>(ItemNames itemID) where T : ItemBase {

		ItemBase item = null;
		Items.TryGetValue(itemID, out item);

		return item != null ? item as T : null;
	}


	public static Sprite GetSprite(ItemNames itemID) {
		Debug.Log(itemID);
		return Items[itemID].Icon;
	}

	public static IEnumerable<T> GetItems<T>() where T : ItemBase {
		foreach (ItemBase item in Items.Values) {
			if (!(item is T)) continue;

			yield return (T)item;
		}
	}

	public static IEnumerable<ItemRecepie> GetRecepies(ItemNames affiliateID = ItemNames.all) {

		foreach (ItemBase item in Items.Values) {
			if (!(item is ItemRecepie)) continue;

			ItemRecepie recepie = (ItemRecepie)item;
			if (affiliateID != ItemNames.all && recepie.AffiliateID != affiliateID) continue;

			yield return recepie;
		}


	}
}
