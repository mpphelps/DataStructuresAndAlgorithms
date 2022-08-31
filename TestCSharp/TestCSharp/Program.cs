namespace test
{
    class Program
    {
        public static void Main(string[] args)
        {
            var dfbSource1 = new FEFExchangeFileFBSource()
            {
                inputParameters = new List<Parameters>()
                {
                    new Parameters() { name = "3D_EN_1", typeName = "BOOL" },
                    new Parameters() { name = "3D_EN_2", typeName = "BOOL" },
                    new Parameters() { name = "MODEL_PARAMS", typeName = "MODEL_PARAMS" },
                    new Parameters() { name = "WORKAREA", typeName = "WORKAREA_REAL" }
                },
                outputParameters = new List<Parameters>()
                {
                    new Parameters() { name = "3D_MODEL", typeName = "MC_DATA" }
                },
                inOutParameters = new List<Parameters>(){},
                FBProgram = new StructuredTest()
                {
                    STSource =
                        "IF 3D_EN_1 OR 3D_EN_2 THEN\n\n\tCAS_3D_MODEL (MODEL_PARAMS := MODEL_PARAMS,\n\t              REQ_PARAMS   := WORKAREA,\n\t              OUT => 3D_MODEL);\n\nELSE \n\n\tINIT_MC_DATA (MC_DATA := 3D_MODEL);\n\nEND_IF;\n"
                }
            };
            var dfbSource2 = new FEFExchangeFileFBSource()
            {
                inputParameters = new List<Parameters>(){},
                outputParameters = new List<Parameters>(){},
                inOutParameters = new List<Parameters>(){ new Parameters(){name = "MC_DATA", typeName = "MC_DATA"} },
                privateLocalVariables = new List<Parameters>()
                {
                    new Parameters(){name = "i", typeName = "INT"},
                    new Parameters(){name = "MODEL_US_EMPTY", typeName = "WA_MODEL_IS_EMPTY"},
                    new Parameters(){name = "MC_DATA_IS_EMPTY", typeName = "BOOL"}
                },
                FBProgram = new StructuredTest()
                {
                    STSource =
                        "IF 3D_EN_1 OR 3D_EN_2 THEN\n\n\tCAS_3D_MODEL (MODEL_PARAMS := MODEL_PARAMS,\n\t              REQ_PARAMS   := WORKAREA,\n\t              OUT => 3D_MODEL);\n\nELSE \n\n\tINIT_MC_DATA (MC_DATA := 3D_MODEL);\n\nEND_IF;\n"
                }
            };
            FormatVariableNameIfSameAsTypeName(dfbSource1);
        }

        static void FormatVariableNameIfSameAsTypeName(FEFExchangeFileFBSource dfbSource)
        {
            var invalidNames = new List<string>();
            var invalidNameSuffix = "_001";
            // Go through function block I/O parameter lists first and fix any variable names that have same name as the typename
            // There could be an edge case issue where the variable is renamed to something that already exists.
            // ie. MC_DATA ==> MC_DATA_001 would be an issue if this already exists.
            if (dfbSource.inputParameters != null)
                foreach (var inputParameter in dfbSource.inputParameters)
                    if (inputParameter.name == inputParameter.typeName)
                    {
                        invalidNames.Add(inputParameter.name);
                        inputParameter.name += invalidNameSuffix;
                    }
            if (dfbSource.outputParameters != null)
                foreach (var outputParameter in dfbSource.outputParameters)
                    if (outputParameter.name == outputParameter.typeName)
                    {
                        invalidNames.Add(outputParameter.name);
                        outputParameter.name += invalidNameSuffix;
                    }
            if (dfbSource.inOutParameters != null)
                foreach (var inOutParameter in dfbSource.inOutParameters)
                    if (inOutParameter.name == inOutParameter.typeName)
                    {
                        invalidNames.Add(inOutParameter.name);
                        inOutParameter.name += invalidNameSuffix;
                    }
            if (dfbSource.privateLocalVariables != null)
                foreach (var privateLocalVariable in dfbSource.privateLocalVariables)
                    if (privateLocalVariable.name == privateLocalVariable.typeName)
                    {
                        invalidNames.Add(privateLocalVariable.name);
                        privateLocalVariable.name += invalidNameSuffix;
                    }

            // Break out if no invalid names found or FB has no source code
            if (!invalidNames.Any() || dfbSource.FBProgram.STSource == null) return;

            // ascii characters that can before or after a variable name.  This used to prevent from accidentally renaming a variable with a similar name
            var asciiComparisonTable = new HashSet<int>()
            {
                11,12,13,15,32,33,34,35,36,37,38,39,40,41,42,43,44,45,46,47,58,59,60,61,62,91,92,93,94,96,123,124,125,126
            };

            foreach (var invalidName in invalidNames)
            {
                var index = dfbSource.FBProgram.STSource.IndexOf(invalidName);
                while (index > -1)
                {
                    var leftChar = dfbSource.FBProgram.STSource[index - 1];
                    var rightChar = dfbSource.FBProgram.STSource[index + invalidName.Length];
                    if (asciiComparisonTable.Contains(leftChar) && asciiComparisonTable.Contains(rightChar))
                    {
                        // Add 'chars before variable name' + 'modified variable name' 'chars after variable name'
                        dfbSource.FBProgram.STSource =
                            dfbSource.FBProgram.STSource.Substring(0, index) +
                            invalidName + invalidNameSuffix +
                            dfbSource.FBProgram.STSource.Substring(index + invalidName.Length,
                                dfbSource.FBProgram.STSource.Length - index - invalidName.Length);
                        index += invalidName.Length + invalidNameSuffix.Length;
                    }
                    index = dfbSource.FBProgram.STSource.IndexOf(invalidName, index);
                }
            }

            Console.WriteLine(dfbSource.FBProgram.STSource);
        }
    }
    class FEFExchangeFileFBSource
    {
        public List<Parameters> inputParameters { get; set; }
        public List<Parameters> outputParameters { get; set; }
        public List<Parameters> inOutParameters { get; set; }
        public List<Parameters> privateLocalVariables { get; set; }
        public StructuredTest FBProgram { get; set; }
    }
    class Parameters
    {
        public string name { get; set; }
        public string typeName { get; set; }
    }
    class StructuredTest
    {
        public string STSource { get; set; }
    }
}



