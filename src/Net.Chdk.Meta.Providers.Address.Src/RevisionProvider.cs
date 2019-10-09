﻿using Microsoft.Extensions.Logging;
using Net.Chdk.Meta.Model.Address;
using Net.Chdk.Meta.Providers.Src;

namespace Net.Chdk.Meta.Providers.Address.Src
{
    sealed class RevisionProvider : RevisionProvider<AddressRevisionData, RevisionData>
    {
        private RevisionAddressProvider RevisionAddressProvider { get; }
        private AddressProvider AddressProvider { get; }

        public RevisionProvider(SourceProvider sourceProvider, DataProvider dataProvider, RevisionAddressProvider revisionAddressProvider, AddressProvider addressProvider, ILogger<RevisionProvider> logger)
            : base(sourceProvider, dataProvider, logger)
        {
            RevisionAddressProvider = revisionAddressProvider;
            AddressProvider = addressProvider;
        }

        protected override AddressRevisionData GetRevisionData(string platformPath, string platform, string revision, RevisionData? data)
        {
            var id = data?.Id;
            if (id != null)
                Logger.LogTrace("ID: {0}", id);

            var revisionAddress = GetRevisionAddress(platformPath, platform, revision);
            var addresses = GetAddresses(platformPath, platform, revision);
            var paletteBufferPtr = addresses?.PaletteBufferPtr;
            if (paletteBufferPtr != null)
                Logger.LogTrace("Palette Buffer Pointer: {0}", paletteBufferPtr);
            uint? activePaletteBuffer = addresses?.ActivePaletteBuffer;
            if (activePaletteBuffer != null)
                Logger.LogTrace("Active Palette Buffer: {0}", activePaletteBuffer);

            return new AddressRevisionData
            {
                Id = id,
                RevisionAddress = revisionAddress,
                PaletteBufferPtr = paletteBufferPtr,
                ActivePaletteBuffer = activePaletteBuffer
            };
        }

        private uint GetRevisionAddress(string platformPath, string platform, string revision)
        {
            return RevisionAddressProvider.GetRevisionAddress(platformPath, platform, revision);
        }

        private AddressData? GetAddresses(string platformPath, string platform, string revision)
        {
            return AddressProvider.GetAddresses(platformPath, platform, revision);
        }
    }
}
