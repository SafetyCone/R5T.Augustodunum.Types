using System;
using System.Collections.Generic;
using System.IO;

using R5T.Magyar.IO;
using R5T.Worcester;


namespace R5T.Augustodunum.Types.IO
{
    /// <summary>
    /// Reads <see cref="RepositoryUrlToLocalRelativeDirectoryPathMapping"/>s from text (useful with the <see cref="FileSerializer"/>).
    /// Each mapping is on a separate line, with the relative path and repository URL separated using the <see cref="MappingsTextSerializer.TokenSeparator"/>.
    /// The relative path is the first token, and the repository URL is the second token.
    /// </summary>
    public class MappingsTextSerializer : ITextSerializer<IEnumerable<RepositoryUrlToLocalRelativeDirectoryPathMapping>>
    {
        public const char TokenSeparator = '|'; // Chosen since it is one of the illegal path characters shown by Windows Explorer when trying to name a file or folder, and it is rarely used in URLs.


        #region Static

        public static MappingsTextSerializer Instance { get; } = new MappingsTextSerializer();

        #endregion


        public IEnumerable<RepositoryUrlToLocalRelativeDirectoryPathMapping> Deserialize(TextReader reader)
        {
            var mappings = new List<RepositoryUrlToLocalRelativeDirectoryPathMapping>();
            while (!reader.ReadLine(out var line))
            {
                var tokens = line.Split(MappingsTextSerializer.TokenSeparator);

                var relativePath = tokens[0];
                var url = tokens[1];

                var mapping = new RepositoryUrlToLocalRelativeDirectoryPathMapping
                {
                    RepositoryUrl = url,
                    LocalRelativeDirectoryPath = relativePath,
                };

                mappings.Add(mapping);
            }

            return mappings;
        }

        public void Serialize(TextWriter writer, IEnumerable<RepositoryUrlToLocalRelativeDirectoryPathMapping> mappings)
        {
            foreach (var mapping in mappings)
            {
                var line = $"{mapping.LocalRelativeDirectoryPath}{MappingsTextSerializer.TokenSeparator}{mapping.RepositoryUrl}";

                writer.WriteLine(line);
            }
        }
    }
}
