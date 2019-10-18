using System;
using System.Linq;
using Mono.Cecil;
using SDX.Compiler;

public class PatcherDisplayCase : IPatcherMod
{
    public bool Patch(ModuleDefinition module)
    {
        Console.WriteLine("==DisplayCase Patch===");
        //SetAccessLevels(module);
        HookMethods(module);
        return true;

    }
    public bool Link(ModuleDefinition gameModule, ModuleDefinition modModule)
    {

        Console.WriteLine("==DisplayCase Link===");
	    return true;
    }

    //private void AddGameMode(ModuleDefinition vanilla)
    //{
    //    var constructor = vanilla.Types.First(d => d.Name == "GameMode").Methods.First(d => d.Name == ".cctor" && d.IsStatic == true);
    //    var inst = constructor.Body.Instructions;
    //    var pro = constructor.Body.GetILProcessor();
    //    var gameModeConstructor = vanilla.Types.First(d => d.Name == "GameModeEditWorld").Methods.First(d => d.Name == ".ctor");
    //    var i = inst.Last(d => d.OpCode == OpCodes.Stelem_Ref).Next;

    //    inst[0].OpCode = OpCodes.Ldc_I4_4;
    //    pro.InsertBefore(i, Instruction.Create(OpCodes.Dup));
    //    pro.InsertBefore(i, Instruction.Create(OpCodes.Ldc_I4_3));
    //    pro.InsertBefore(i, Instruction.Create(OpCodes.Newobj, gameModeConstructor));
    //    pro.InsertBefore(i, Instruction.Create(OpCodes.Stelem_Ref));

    //}

    private void HookMethods(ModuleDefinition vanilla)
    {

        var gm = vanilla.Types.First(d => d.Name == "GameManager");
        var windowMan = gm.Fields.First(d => d.Name == "windowManager");
        SetFieldToPublic(windowMan);

    }

    private void SetMethodToVirtual(MethodDefinition meth)
    {
        meth.IsVirtual = true;
    }
    private void SetFieldToPublic(FieldDefinition field)
    {
        field.IsFamily = false;
        field.IsPrivate = false;
        field.IsPublic = true;

    }
    private void SetMethodToPublic(MethodDefinition field)
    {
        field.IsFamily = false;
        field.IsPrivate = false;
        field.IsPublic = true;

    }
}
