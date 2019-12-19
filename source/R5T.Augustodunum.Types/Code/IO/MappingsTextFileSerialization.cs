using System;
using System.Collections.Generic;

using R5T.Worcester;


namespace R5T.Augustodunum.Types.IO
{
    public static class MappingsTextFileSerialization
    {
        public static IEnumerable<RepositoryUrlToLocalRelativeDirectoryPathMapping> Deserialize(string mappingsTextFilePath)
        {
            var mappings = FileSerializer.Deserialize(mappingsTextFilePath, MappingsTextSerializer.Instance);
            return mappings;
        }

        public static void Serialize(string mappingsTextFilePath, IEnumerable<RepositoryUrlToLocalRelativeDirectoryPathMapping> mappings, bool overwrite = true)
        {
            FileSerializer.Serialize(mappingsTextFilePath, mappings, MappingsTextSerializer.Instance, overwrite);
        }
    }
}
