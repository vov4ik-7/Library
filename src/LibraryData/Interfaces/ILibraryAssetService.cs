﻿using LibraryData.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryData.Interfaces
{
    public interface ILibraryAssetService
    {
        IEnumerable<LibraryAsset> GetAll();
        LibraryAsset Get(int id);

        void Add(LibraryAsset newAsset);
        string GetAuthorOrDirector(int id);
        string GetDeweyIndex(int id);
        string GetType(int id);
        string GetTitle(int id);
        string GetIsbn(int id);

        LibraryBranch GetCurrentLocation(int id);
        LibraryCard GetLibraryCardByAssetId(int id);
    }
}
