using System;
using System.Collections.Generic;
using System.Numerics;

namespace ModularRobot
{
    public class Module: IOrientation
    {
        private Guid _uuid;
        private Dictionary<int, AttachmentPoint> _attachmentPoints;
        private Dictionary<int, Module> _children;
        private Quaternion _orientation;
        private Module? _parent;
        private int? _parentChildIndex;
        private AttachedSensors _sensors;
        private Color _color;

        public Guid Uuid => _uuid;
        public Quaternion Orientation => _orientation;
        public Module? Parent { get => _parent; set => _parent = value; }
        public int? ParentChildIndex { get => _parentChildIndex; set => _parentChildIndex = value; }
        public Dictionary<int, Module> Children => _children;
        public AttachedSensors Sensors => _sensors;
        public Color Color { get => _color; set => _color = value; }
        public Dictionary<int, AttachmentPoint> AttachmentPoints => _attachmentPoints;

        public Module(
            Quaternion orientation,
            Color color,
            Dictionary<int, AttachmentPoint> attachmentPoints,
            List<Sensor> sensors
        )
        {
            _uuid = Guid.NewGuid();
            _parent = null;
            _parentChildIndex = null;
            _children = new Dictionary<int, Module>();
            _orientation = orientation;
            _color = color;
            _attachmentPoints = attachmentPoints;
            _sensors = new AttachedSensors();
            foreach (var sensor in sensors)
            {
                _sensors.AddSensor(sensor);
            }
        }

        public virtual void SetChild(Module module, int childIndex)
        {
            if (module._parent != null)
                throw new InvalidOperationException("Child module already connected to another slot.");

            module._parent = this;
            module._parentChildIndex = childIndex;

            if (CanSetChild(childIndex))
                _children[childIndex] = module;
            else
                throw new KeyNotFoundException("Attachment point already populated.");
        }

        public virtual bool CanSetChild(int childIndex)
        {
            return !_children.ContainsKey(childIndex);
        }

        public List<Module> Neighbours(int withinRange)
        {
            var outNeighbours = new List<Module>();
            var openNodes = new List<(Module, Module?)> { (this, null) };

            for (int i = 0; i < withinRange; i++)
            {
                var newOpenNodes = new List<(Module, Module?)>();
                foreach (var (openNode, cameFrom) in openNodes)
                {
                    var attachedModules = new List<Module>();
                    foreach (var index in openNode.AttachmentPoints.Keys)
                    {
                        if (openNode.Children.ContainsKey(index))
                            attachedModules.Add(openNode.Children[index]);
                    }

                    var neighbours = attachedModules.FindAll(
                        mod => mod != null && (cameFrom == null || mod.Uuid != cameFrom.Uuid));

                    outNeighbours.AddRange(neighbours);
                    newOpenNodes.AddRange(neighbours.ConvertAll(neighbour => (neighbour, openNode)));
                }

                openNodes = newOpenNodes;
            }

            return outNeighbours;
        }

        public void AddSensor(Sensor sensor)
        {
            _sensors.AddSensor(sensor);
        }
    }

}
