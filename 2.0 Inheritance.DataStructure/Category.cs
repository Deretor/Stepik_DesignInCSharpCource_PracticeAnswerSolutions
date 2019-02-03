using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inheritance.DataStructure
{
    class Category : IComparable
    {

        public readonly string Name;
        public readonly MessageType Type;
        public readonly MessageTopic Topic;

        public Category(string name, MessageType type, MessageTopic topic)
        {
            this.Name = name;
            this.Type = type;
            this.Topic = topic;

        }
        public override string ToString()
        {
            var r1 =  $"{Name}.{Type}.{Topic}";
            return r1;
        }

        public int CompareTo(object obj)
        {
            if(obj == null)
            {
                return 1;
            }
            if (obj is Category)
            {
                var cobj = (Category)obj;
                var n =  Name?.CompareTo(cobj.Name) ?? -1;
                var ty = Type.CompareTo(cobj.Type);
                var to = Topic.CompareTo(cobj.Topic);
                var result = n != 0 ? n : (ty != 0 ? ty : to);
                return result;
            }
            else {
                throw new ArgumentException($"Can not compate Category with {obj.GetType().Name}");
            }
            
        }

        public override bool Equals(object obj)
        {
            bool a = obj is Category;
            if (a)
            {
                Category b = obj as Category;
                if (b == null) return this == null;
                return Name == b.Name && Topic == b.Topic && Type == b.Type;
            }
            else {
                return false;
            }
        }

        public override int GetHashCode()
        {
            var a = this.Name.GetHashCode();
            return a ^ ((int)this.Topic + (int)this.Type);
        }

        public static bool operator <(Category a, Category b) {
            return a.CompareTo(b) < 0;
        }
        public static bool operator >(Category a, Category b) {
            return a.CompareTo(b) > 0;
        }
        public static bool operator <= (Category a, Category b)
        {
            return a.CompareTo(b) <= 0;
        }
        public static bool operator >= (Category a, Category b)
        {
            return a.CompareTo(b) >= 0;
        }
    }

}
