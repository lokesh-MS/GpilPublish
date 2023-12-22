#region File Header
/*
--------------------------------------
TeamLiftss IT Systems & solutions pvt. ltd.
Copyright (c) 2021, All rights reserved

Author      : ANANDARAJ G 
Description : Base method implemented. 

Revision History:
Rev   Date                   Who                    Description
1.0   28/July/2021          Anandaraj G            Intial version.
--------------------------------------
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GPI
{
    /// <summary>
    /// Interface that has basic database functions to be implemented.
    /// </summary>
    internal interface IBaseDB
    {
        /// <summary>
        /// Basic save method of the current object.
        /// </summary>
        void Save();
        /// <summary>
        /// Basic delete method of the current object.
        /// </summary>
        void Delete();
        /// <summary>
        /// Basic update method of the current object.
        /// </summary>
        void Update();
    }
}