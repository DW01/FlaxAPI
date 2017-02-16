﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assert = FlaxEngine.Assertions.Assert;


namespace FlaxEditor.Tests
{
    [TestClass]
    public class UndoTests
    {
        [Serializable]
        public class UndoObject
        {
            public int FieldInteger = 10;
            public float FieldFloat = 0.1f;
            public UndoObject FieldObject;

            public int PropertyInteger { get; set; } = -10;
            public float PropertyFloat { get; set; } = -0.1f;
            public UndoObject PropertyObject { get; set; }

            public UndoObject()
            {
            }

            public UndoObject(bool addInstance)
            {
                if (!addInstance)
                {
                    return;
                }
                FieldObject = new UndoObject
                {
                    FieldInteger = 1,
                    FieldFloat = 1.1f,
                    FieldObject = new UndoObject(),
                    PropertyFloat = 1.1f,
                    PropertyInteger = 1,
                    PropertyObject = null
                };
                PropertyObject = new UndoObject
                {
                    FieldInteger = -1,
                    FieldFloat = -1.1f,
                    FieldObject = null,
                    PropertyFloat = -1.1f,
                    PropertyInteger = -1,
                    PropertyObject = new UndoObject()
                };
            }
        }

        [TestMethod]
        public void UndoTestBasic()
        {
            var instance = new UndoObject(true);
            Undo.RecordBegin(instance, "Basic");
            instance.FieldFloat = 0;
            instance.FieldInteger = 0;
            instance.FieldObject = null;
            instance.PropertyFloat = 0;
            instance.PropertyInteger = 0;
            instance.PropertyObject = null;
            Undo.RecordEnd();
            var id = BasicUndoRedo(instance, new Guid());

            instance = new UndoObject(true);
            Undo.RecordAction(instance, "Basic", () =>
            {
                instance.FieldFloat = 0;
                instance.FieldInteger = 0;
                instance.FieldObject = null;
                instance.PropertyFloat = 0;
                instance.PropertyInteger = 0;
                instance.PropertyObject = null;
            });
            id = BasicUndoRedo(instance, id);

            object generic = new UndoObject(true);
            Undo.RecordAction(generic, "Basic", (i) =>
            {
                ((UndoObject)i).FieldFloat = 0;
                ((UndoObject)i).FieldInteger = 0;
                ((UndoObject)i).FieldObject = null;
                ((UndoObject)i).PropertyFloat = 0;
                ((UndoObject)i).PropertyInteger = 0;
                ((UndoObject)i).PropertyObject = null;
            });
            id = BasicUndoRedo((UndoObject)generic, id);

            instance = new UndoObject(true);
            Undo.RecordAction(instance, "Basic", (i) =>
            {
                i.FieldFloat = 0;
                i.FieldInteger = 0;
                i.FieldObject = null;
                i.PropertyFloat = 0;
                i.PropertyInteger = 0;
                i.PropertyObject = null;
            });
            id = BasicUndoRedo(instance, id);

            instance = new UndoObject(true);
            using(new Undo(instance, "Basic"))
            {
                instance.FieldFloat = 0;
                instance.FieldInteger = 0;
                instance.FieldObject = null;
                instance.PropertyFloat = 0;
                instance.PropertyInteger = 0;
                instance.PropertyObject = null;
            }
            id = BasicUndoRedo(instance, id);
        }

        private static Guid BasicUndoRedo(UndoObject instance, Guid lastGuid)
        {
            Assert.AreEqual("Basic", Undo.PerformUndo().ActionString);
            Assert.AreEqual(10, instance.FieldInteger);
            Assert.AreEqual(0.1f, instance.FieldFloat);
            Assert.AreNotEqual(null, instance.FieldObject);
            Assert.AreEqual(-10, instance.PropertyInteger);
            Assert.AreEqual(-0.1f, instance.PropertyFloat);
            Assert.AreNotEqual(null, instance.PropertyObject);
            var redo = Undo.PerformRedo();
            Assert.AreNotEqual(redo.Id, lastGuid);
            Assert.AreEqual("Basic", redo.ActionString);
            Assert.AreEqual(0, instance.FieldInteger);
            Assert.AreEqual(0, instance.FieldFloat);
            Assert.AreEqual(null, instance.FieldObject);
            Assert.AreEqual(0, instance.PropertyInteger);
            Assert.AreEqual(0, instance.PropertyFloat);
            Assert.AreEqual(null, instance.PropertyObject);
            return redo.Id;
        }

        [TestMethod]
        public void UndoTestRecursive()
        {
            var instance = new UndoObject(true);
            Undo.RecordAction(instance, "Basic", (i) =>
            {
                i.FieldObject = new UndoObject();
                i.FieldObject.FieldObject = new UndoObject();
                i.FieldObject.FieldObject.FieldObject = new UndoObject();
                i.FieldObject.FieldObject.PropertyObject = new UndoObject();
                i.FieldObject.FieldObject.PropertyObject.FieldInteger = 99;
                i.PropertyObject = new UndoObject();
                i.PropertyObject.PropertyObject = new UndoObject();
                i.PropertyObject.PropertyObject.PropertyObject = new UndoObject();
                i.PropertyObject.PropertyObject.FieldObject = new UndoObject();
                i.PropertyObject.PropertyObject.FieldObject.FieldInteger = 99;
            });
            Undo.PerformUndo();
            Assert.AreNotEqual(null, instance.FieldObject);
            Assert.AreNotEqual(null, instance.FieldObject.FieldObject);
            Assert.AreEqual(null, instance.FieldObject.FieldObject.FieldObject);
            Assert.AreEqual(null, instance.FieldObject.FieldObject.PropertyObject);
            Assert.AreNotEqual(null, instance.PropertyObject);
            Assert.AreNotEqual(null, instance.PropertyObject.PropertyObject);
            Assert.AreEqual(null, instance.PropertyObject.PropertyObject.PropertyObject);
            Assert.AreEqual(null, instance.PropertyObject.PropertyObject.FieldObject);
            Undo.PerformRedo();
            Assert.AreNotEqual(null, instance.FieldObject);
            Assert.AreNotEqual(null, instance.FieldObject.FieldObject);
            Assert.AreNotEqual(null, instance.FieldObject.FieldObject.FieldObject);
            Assert.AreNotEqual(null, instance.FieldObject.FieldObject.PropertyObject);
            Assert.AreNotEqual(null, instance.PropertyObject);
            Assert.AreNotEqual(null, instance.PropertyObject.PropertyObject);
            Assert.AreNotEqual(null, instance.PropertyObject.PropertyObject.PropertyObject);
            Assert.AreNotEqual(null, instance.PropertyObject.PropertyObject.FieldObject);
            Assert.AreEqual(99, instance.FieldObject.FieldObject.PropertyObject.FieldInteger);
            Assert.AreEqual(99, instance.PropertyObject.PropertyObject.FieldObject.FieldInteger);
        }
    }
}