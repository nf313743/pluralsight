﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DddInPractice.Logic
{
    public class Snack : AggregateRoot
    {
        public virtual string Name { get; protected set; }

        protected Snack()
        {
        }

        public Snack(string name) : this()
        {
            Name = name;
        }
    }
}