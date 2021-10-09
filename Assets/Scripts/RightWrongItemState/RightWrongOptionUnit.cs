using Bolt;
using Ludiq;
using UnityEngine;


[UnitTitle("RightWrongOption")]
[UnitCategory("RightWrongOption")]
public class RightWrongOptionUnit : Unit
{

    [DoNotSerialize]
    public ValueInput gameObject;

    [DoNotSerialize]
    public ControlInput controlInput;

    [DoNotSerialize]
    public ControlOutput rightOut;

    [DoNotSerialize]
    public ControlOutput wrongOut;

    protected override void Definition()
    {

        gameObject = ValueInput<GameObject>("gameObject");

        controlInput = ControlInput("Input", Action);

        rightOut = ControlOutput("Right");

        wrongOut = ControlOutput("Wrong");

        Requirement(gameObject, controlInput);

        Succession(controlInput, rightOut);

        Succession(controlInput, wrongOut);
    }

    ControlOutput Action(Flow flow)
    {

        var gb = flow.GetValue<GameObject>(gameObject);

        if (gb == null) return wrongOut;

        var attachment = gb.GetComponent<RightWrongOptionAttachment>();

        if (attachment == null) return wrongOut;

        return attachment.isTheRightOption ? rightOut : wrongOut;

    }
}