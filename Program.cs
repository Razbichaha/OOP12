using System;
using System.Collections.Generic;

namespace OOP12
{
    //    Пользователь запускает приложение и перед ним находится меню,
    //    в котором он может выбрать, к какому вольеру подойти.
    //    При приближении к вольеру, пользователю выводится информация о том, что это за вольер,
    //    сколько животных там обитает, их пол и какой звук издает животное.
    //Вольеров в зоопарке может быть много, в решении нужно создать минимум 4 вольера
    class Program
    {
        static void Main(string[] args)
        {
            ProgramCore programCore = new ProgramCore();
            programCore.ProgramLogic();
            Console.ReadLine();
        }
    }

    class ProgramCore
    {
        private Zoo _zoo = new Zoo();
        private Menu _menu = new Menu();

        internal void ProgramLogic()
        {
            _menu.ShowHeader();
            _zoo.PresentZoo();
            GoAviary();
        }

        private void GoAviary()
        {
            string aviaryString = _menu.ChoosingAviary();

            if (_zoo.ThereIsAviary(aviaryString))
            {
                _menu.ShowAviary(aviaryString);
                _zoo.ShowAviares(aviaryString);
            }
            else
            {
                _menu.ErrorEnter();
            }
        }
    }

    class Menu
    {
        internal void ShowAnimal(Animal animal)
        {
            Console.WriteLine($"Парода - {animal.Name}   пол животного - {animal.Gender}   голос - {animal.Sound}");
        }

        internal void ShowAviary(string Animals)
        {
            Console.Clear();
            Console.WriteLine($"Вы подошли к вольеру с {Animals}");
            Console.WriteLine();
        }

        internal void ShowHeader()
        {
            Console.WriteLine("Мы представляем вам наш зоопарк\nвы можете посетить вольеры с животными");
            Console.WriteLine("Выберите какой из пердоставленных вольеров вы хотите посетить");
        }

        internal void ShowAviaries(string name)
        {
            Console.WriteLine($"Вольер с {name}");
        }

        internal string ChoosingAviary()
        {
            Console.WriteLine("Выберите вольер для просмотра.");
            string choosing = Console.ReadLine();

            return choosing;
        }

        internal void ErrorEnter()
        {
            Console.WriteLine("Не верный ввод, попробуйте ещё раз.");
        }
    }

    class Zoo
    {
        private Menu _menu = new Menu();
        private List<Aviary> _ListAviares = new List<Aviary>();

        private int _maximumAviary = 10;
        private int _minimumAviary = 4;


        internal Zoo()
        {
            Create();
        }

        internal void ShowAviares(string aviary)
        {
            foreach (Aviary item in _ListAviares)
            {
                if (item.Name == aviary)
                {
                    item.ShowAnimals(item.Name);
                    break;
                }
            }
        }

        internal void PresentZoo()
        {
            foreach (Aviary aviary in _ListAviares)
            {
                _menu.ShowAviaries(aviary.Name);
            }
        }

        internal bool ThereIsAviary(string aviary)
        {
            bool isMatch = false;

            foreach (Aviary item in _ListAviares)
            {
                if (item.Name == aviary)
                {
                    isMatch = true;
                }
            }
            return isMatch;
        }

        private void Create()
        {
            Random random = new Random();
            int randomQuantityAaviary = random.Next(_minimumAviary, _maximumAviary);

            for (int i = 0; i < randomQuantityAaviary; i++)
            {
                Aviary aviary = new Aviary();

                if (IsMatch(aviary) == false)
                {
                    _ListAviares.Add(aviary);
                }
            }
        }

        private bool IsMatch(Aviary aviary)
        {
            bool match = false;

            foreach (Aviary tempAviary in _ListAviares)
            {
                if (tempAviary.Name == aviary.Name)
                {
                    match = true;
                }
            }
            return match;
        }
    }

    class Aviary
    {
        private string[] _listAviary = { "Медведями", "Тиграми", "Зайцами", "Собаками", "Кошками", "Пантерами", "Страусами", "Крокодилами", "Чебурашками" };
        private int _maximumAnimals = 10;
        private int _minimumAnimals = 3;
        private List<Animal> _listAnimals = new List<Animal>();
        private List<int> _tempBaseId = new List<int>(0);
        private Random _random = new Random();
        private Menu _menu = new Menu();

        internal string Name { get; private set; }

        public Aviary()
        {
            Create();
        }

        internal void Create()
        {
            Name = GenerateName();
            GenerateAnimals();
        }

        internal void ShowAnimals(string name)
        {
            foreach (Animal animal in _listAnimals)
            {
                _menu.ShowAnimal(animal);
            }
        }

        private void GenerateAnimals()
        {
            int randomQuantityAnimals = _random.Next(_minimumAnimals, _maximumAnimals);

            for (int i = 0; i < randomQuantityAnimals; i++)
            {
                Animal animal = new Animal(GetIndex(Name));
                _listAnimals.Add(animal);
            }
        }

        private int GetIndex(string name)
        {
            int index = 0;

            for (int i = 0; i < _listAviary.Length; i++)
            {
                if (_listAviary[i] == name)
                {
                    index = i;
                }
            }
            return index;
        }

        private string GenerateName()
        {
            bool thereIsId = false;
            string name = "";
            int numberAviary = _random.Next(_listAviary.Length);

            foreach (int index in _tempBaseId)
            {
                if (index == numberAviary)
                {
                    thereIsId = true;
                }
            }

            if (thereIsId == false)
            {
                name = _listAviary[numberAviary];
                _tempBaseId.Add(numberAviary);
            }
            return name;
        }
    }

    class Animal
    {
        private string[] _listAnimal = { "Медведь", "Тигр", "Заяц", "Собака", "Кошка", "Пантера", "Страус", "Крокодил", "Чебурашка" };
        private string[] _listAnimalSounds = { "ЫЫЫ", "РРРР", "тратата", "гав гав", "мяуу", "мурррыы", "шшшш", "клац", "хочу в артелиристы" };
        private string[] _listGenders = { "Самка", "Самец" };

        private Random _random = new Random();

        internal string Name { get; private set; }

        internal string Gender { get; private set; }

        internal string Sound { get; private set; }

        internal Animal(int nameIndex)
        {
            Name = _listAnimal[nameIndex];
            Create();
        }

        private void Create()
        {
            GenerateSound();
            GenerateGender();
        }

        private void GenerateGender()
        {
            Gender = _listGenders[_random.Next(0, _listGenders.Length)];
        }

        private void GenerateSound()
        {
            for (int i = 0; i < _listAnimal.Length; i++)
            {
                if (Name == _listAnimal[i])
                {
                    Sound = _listAnimalSounds[i];
                }
            }
        }
    }
}
