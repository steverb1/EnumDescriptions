using System;
using System.Linq;
using System.Reflection;
using NUnit.Framework;

namespace EnumDescriptions.Tests
{
    public class EnumDescriptionTests
    {
        [Test]
        public void A319Description_Returns319()
        {
            AirbusEquipmentTypes a319 = AirbusEquipmentTypes.A319;
            Assert.AreEqual("319", a319.GetDescription());
        }

        [Test]
        public void A321String_IsFound()
        {
            AirbusEquipmentTypes a321 = AirbusEquipmentTypes.A321;

            Assert.IsTrue(FindMatch("321"));
        }

        [Test]
        public void A321xyzString_IsNotFound()
        {
            AirbusEquipmentTypes a321 = AirbusEquipmentTypes.A321;

            Assert.IsFalse(FindMatch("321xyz"));
        }

        private bool FindMatch(string equipmentType)
        {
            return Enum.GetValues(typeof(AirbusEquipmentTypes)).OfType<AirbusEquipmentTypes>().Any(f => f.GetDescription().Equals(equipmentType));
        }
    }

    public static class EnumExtensions
    {
        public static string GetDescription(this Enum source)
        {
            FieldInfo fi = source.GetType().GetField(source.ToString());

            System.ComponentModel.DescriptionAttribute[] attributes = (System.ComponentModel.DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(System.ComponentModel.DescriptionAttribute), false);

            return attributes.Length > 0 ? attributes[0].Description : source.ToString();
        }
    }
}
