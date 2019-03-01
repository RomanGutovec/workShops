using System;
using System.Collections.Generic;
using System.Linq;

public class Program
{
    public class DbEntity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1}, age {2}", FirstName, LastName, Age);
        }
    }

    public class DbGenerator
    {
        private static Random _random = new Random((int)DateTime.Now.Ticks);

        private static string[] _firstNames = new string[]
        {
            "Oliver",
            "Harry",
            "Jack",
            "George",
            "Noah",
            "Charlie",
            "Jacob",
            "Alfie",
            "Freddie"
        };

        private static string[] _lastNames = new string[]
        {
            "Smith",
            "Johnson",
            "Williams",
            "Jones",
            "Brown",
            "Davis",
            "Miller"
        };

        public IEnumerable<DbEntity> GetSequence(int count)
        {
            for (int i = 0; i < count; i++)
            {
                yield return new DbEntity()
                {
                    FirstName = _firstNames[_random.Next(_firstNames.Length - 1)],
                    LastName = _lastNames[_random.Next(_lastNames.Length - 1)],
                    Age = _random.Next(18, 60)
                };
            }
        }
    }

    public struct FirstLastKey
    {
        private readonly string _firstName;
        private readonly string _lastName;

        public FirstLastKey(string firstName, string lastName)
        {
            _firstName = firstName;
            _lastName = lastName;
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() != this.GetType() || obj == null)
            {
                return false;
            }
            FirstLastKey key = (FirstLastKey)obj;
            return this._firstName == key._firstName && this._lastName == key._lastName;
        }

        public override int GetHashCode()
        {   
            int hash = 17;
            hash = hash * 31 + _firstName.GetHashCode();
            hash = hash * 31 + _lastName.GetHashCode();

            return hash;
        }
    }

    public class Database
    {
        private readonly List<DbEntity> _entities = new List<DbEntity>();
        private readonly Dictionary<FirstLastKey, List<DbEntity>> _fnDict = new Dictionary<FirstLastKey, List<DbEntity>>();

        public void AddRange(IEnumerable<DbEntity> entities)
        {
            _entities.AddRange(entities);

            foreach (var item in entities)
            {
                if (!_fnDict.ContainsKey(new FirstLastKey(item.FirstName, item.LastName)))
                {
                    _fnDict.Add(new FirstLastKey(item.FirstName, item.LastName), new DbGenerator().GetSequence(3).ToList());
                }
            }
        }

        public IList<DbEntity> FindBy(string firstName, string lastName)
        {

            List<DbEntity> resultEntities = new List<DbEntity>();

            if (_fnDict.TryGetValue(new FirstLastKey(firstName, lastName), out resultEntities))
            {
                return resultEntities;
            }

            return null;
        }

        public IList<DbEntity> FindBy(int age)
        {
            List<DbEntity> resultEntities = new List<DbEntity>();

            foreach (var item in _fnDict)
            {
                resultEntities.AddRange(item.Value.Where(i => i.Age == age));
            }

            return resultEntities;
        }
    }

    public static void Main()
    {
        var dbGenerator = new DbGenerator();
        var db = new Database();
        db.AddRange(dbGenerator.GetSequence(10000));

        var items = db.FindBy("Jack", "Jones");
        Console.WriteLine(items.Count);

        var items2 = db.FindBy(30);
        Console.WriteLine(items2.Count);
    }
}