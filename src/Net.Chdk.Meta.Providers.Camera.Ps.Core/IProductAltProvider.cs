﻿using Net.Chdk.Meta.Model.Camera.Ps;

namespace Net.Chdk.Meta.Providers.Camera.Ps
{
    public interface IProductAltProvider : IProductNameProvider
    {
        AltData GetAlt(string platform, string[]? altNames);
    }
}
