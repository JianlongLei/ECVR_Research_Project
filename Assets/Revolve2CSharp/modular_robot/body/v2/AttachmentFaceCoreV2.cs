using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Revolve2.Robot
{
    /// <summary>
    /// Represents an AttachmentFace for the V2 Core.
    /// </summary>
    public class AttachmentFaceCoreV2 : AttachmentFace
    {
        private readonly int[,] _checkMatrix = new int[3, 3];
        private readonly Vector3 _childOffset;

        /// <summary>
        /// Initializes a new instance of the <see cref="AttachmentFaceCoreV2"/> class.
        /// </summary>
        /// <param name="faceRotation">The rotation of the face and attachment points.</param>
        /// <param name="horizontalOffset">The horizontal offset for module placement.</param>
        /// <param name="verticalOffset">The vertical offset for module placement.</param>
        public AttachmentFaceCoreV2(float faceRotation, float horizontalOffset, float verticalOffset)
            : base(
                0.0f,
                CreateAttachmentPoints(faceRotation, horizontalOffset, verticalOffset)
            )
        {
            _childOffset = new Vector3(0.15f / 2.0f, 0.0f, 0.0f);
        }

        private static Dictionary<int, AttachmentPoint> CreateAttachmentPoints(float faceRotation, float horizontalOffset, float verticalOffset)
        {
            var attachmentPoints = new Dictionary<int, AttachmentPoint>();
            var rot = Quaternion.CreateFromAxisAngle(Vector3.UnitZ, faceRotation);

            for (int i = 0; i < 9; i++)
            {
                float h_o = (i % 3 - 1) * horizontalOffset;
                float v_o = -(i / 3 - 1) * verticalOffset;

                if ((int)(rot.W / MathF.PI) % 2 == 0) h_o = -h_o;
                var offset = (MathF.Abs(rot.W % MathF.PI) < 1e-5)
                    ? new Vector3(0.0f, h_o, v_o)
                    : new Vector3(h_o, 0.0f, v_o);

                offset = Vector3.Transform(offset, rot.ToMatrix());

                attachmentPoints[i] = new AttachmentPoint(
                    rot,
                    new Vector3(0.15f / 2.0f, 0.0f, 0.0f) + offset
                );
            }

            return attachmentPoints;
        }

        public override void SetChild(Module module, int childIndex)
        {
            if (module.Parent != null)
                throw new InvalidOperationException("Child module already connected to a different slot.");

            if (!CanSetChild(childIndex))
                throw new KeyNotFoundException("Attachment point already populated or occluded by another module.");

            module.Parent = this;
            module.ParentChildIndex = childIndex;
            _checkMatrix[childIndex / 3, childIndex % 3]++;
            Children[childIndex] = module;
        }

        public override bool CanSetChild(int childIndex)
        {
            var checkMatrixCopy = (int[,])_checkMatrix.Clone();
            checkMatrixCopy[childIndex / 3, childIndex % 3]++;

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    int sum = 0;
                    for (int x = 0; x < 2; x++)
                    {
                        for (int y = 0; y < 2; y++)
                        {
                            sum += checkMatrixCopy[i + x, j + y];
                        }
                    }

                    if (sum > 1) return false; // Conflict detected.
                }
            }

            return !Children.ContainsKey(childIndex);
        }

        // Properties for each slot (e.g., topLeft, top, middle, bottom).
        public Module? TopLeft
        {
            get => Children.TryGetValue(0, out var module) ? module : null;
            set => SetChild(value, 0);
        }

        public Module? Top
        {
            get => Children.TryGetValue(1, out var module) ? module : null;
            set => SetChild(value, 1);
        }

        public Module? TopRight
        {
            get => Children.TryGetValue(2, out var module) ? module : null;
            set => SetChild(value, 2);
        }

        public Module? MiddleLeft
        {
            get => Children.TryGetValue(3, out var module) ? module : null;
            set => SetChild(value, 3);
        }

        public Module? Middle
        {
            get => Children.TryGetValue(4, out var module) ? module : null;
            set => SetChild(value, 4);
        }

        public Module? MiddleRight
        {
            get => Children.TryGetValue(5, out var module) ? module : null;
            set => SetChild(value, 5);
        }

        public Module? BottomLeft
        {
            get => Children.TryGetValue(6, out var module) ? module : null;
            set => SetChild(value, 6);
        }

        public Module? Bottom
        {
            get => Children.TryGetValue(7, out var module) ? module : null;
            set => SetChild(value, 7);
        }

        public Module? BottomRight
        {
            get => Children.TryGetValue(8, out var module) ? module : null;
            set => SetChild(value, 8);
        }
    }
}
