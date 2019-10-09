using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Credit to original Fungus devs for this code

namespace Fungus
{
    /// <summary>
    /// Vector3 add, sub, mul, div arithmetic
    /// </summary>
    [CommandInfo("Vector2",
                 "Arithmetic",
                 "Vector2 add, sub, mul, div arithmetic")]
    [AddComponentMenu("")]
    public class Vector2Arithmetic : Command
    {
        [SerializeField]
        protected Vector2Data lhs, rhs, output;

        public enum Operation
        {
            Add,
            Sub,
            Mul,
            Div
        }

        [SerializeField]
        protected Operation operation = Operation.Add;

        public override void OnEnter()
        {
            Vector2 tmp;
            switch (operation)
            {
                case Operation.Add:
                    output.Value = lhs.Value + rhs.Value;
                    break;
                case Operation.Sub:
                    output.Value = lhs.Value - rhs.Value;
                    break;
                case Operation.Mul:
                    tmp = lhs.Value;
                    tmp.Scale(rhs.Value);
                    output.Value = tmp;
                    break;
                case Operation.Div:
                    tmp = lhs.Value;
                    tmp.Scale(new Vector2(1.0f / rhs.Value.x,
                        1.0f / rhs.Value.y));
                    output.Value = tmp;
                    break;
                default:
                    break;
            }
            Continue();
        }

        public override string GetSummary()
        {
            if (output.vector2Ref == null)
            {
                return "Error: no output set";
            }

            return operation.ToString() + ": stored in " + output.vector2Ref.Key;
        }

        public override Color GetButtonColor()
        {
            return new Color32(235, 191, 217, 255);
        }

        public override bool HasReference(Variable variable)
        {
            if (lhs.vector2Ref == variable || rhs.vector2Ref == variable || output.vector2Ref == variable)
                return true;

            return false;
        }
    }
}