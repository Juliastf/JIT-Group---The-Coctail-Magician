﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CM.Services.Contracts
{
    public interface INotificationManager
    {
        string CocktailAddedDescription(string username, string cocktailName);

    }
}