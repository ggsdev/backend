using dotenv.net;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRIO.Migrations
{
    /// <inheritdoc />
    public partial class PopulatePI : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var envVars = DotEnv.Read();

            string instanceKey = envVars["INSTANCE"];

            Console.WriteLine(instanceKey);

            var databaseId = Guid.NewGuid();
            migrationBuilder.InsertData(
               table: "PI.Databases",
               columns: new[] { "Id", "WebId", "PIId", "Name", "Description", "SelfRoute", "ElementsRoute" },
               values: new object[] {
                    databaseId,
                    "F1RDcaZI8jdsuU6iCfbmKdB6iQNLO5kFOj7UO8USTMke5j5wUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9T",
                    Guid.NewGuid().ToString(),
                    "PRIO - Cálculos",
                    "Database PRIO",
                    "https://prrjbsrvvm170.petrorio.local/piwebapi/assetdatabases/F1RDcaZI8jdsuU6iCfbmKdB6iQNLO5kFOj7UO8USTMke5j5wUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9T",
                    "https://prrjbsrvvm170.petrorio.local/piwebapi/assetdatabases/F1RDcaZI8jdsuU6iCfbmKdB6iQNLO5kFOj7UO8USTMke5j5wUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9T/elements"
               });

            if (instanceKey.ToUpper() == "BRAVO")
            {
                var bravoId = Guid.NewGuid();
                var polvoId = Guid.NewGuid();

                var instancesData = new List<object[]>
                    {
                        new object[] {
                            bravoId,
                            "F1EmcaZI8jdsuU6iCfbmKdB6iQ2IKkNIvq7RGxjQBQVoy5FQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk8",
                            "34a482d8-ea8b-11ed-b18d-0050568cb915",
                            "FPSO BRAVO",
                            "Cálculos para TBMT",
                            "https://prrjbsrvvm170.petrorio.local/piwebapi/elements/F1EmcaZI8jdsuU6iCfbmKdB6iQ2IKkNIvq7RGxjQBQVoy5FQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk8",
                            "https://prrjbsrvvm170.petrorio.local/piwebapi/elements/F1EmcaZI8jdsuU6iCfbmKdB6iQ2IKkNIvq7RGxjQBQVoy5FQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk8/elements"
                        },
                        new object[] {
                            polvoId,
                            "F1EmcaZI8jdsuU6iCfbmKdB6iQ2IKkNIvq7RGxjQBQVoy5FQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk8",
                            "3c92e53a-ea8b-11ed-b18d-0050568cb915",
                            "POLVO-A",
                            "Cálculos para Polvo",
                            "https://prrjbsrvvm170.petrorio.local/piwebapi/elements/F1EmcaZI8jdsuU6iCfbmKdB6iQOuWSPIvq7RGxjQBQVoy5FQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUE",
                            "https://prrjbsrvvm170.petrorio.local/piwebapi/elements/F1EmcaZI8jdsuU6iCfbmKdB6iQOuWSPIvq7RGxjQBQVoy5FQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUE/elements"
                        },
                    };

                foreach (var instances in instancesData)
                {
                    var instanceId = (Guid)instances[0];
                    var instanceWebId = instances[1];
                    var instancePIId = instances[2];
                    var instanceName = instances[3];
                    var instanceDescription = instances[4];
                    var instanceSelfRoute = instances[5];
                    var instanceElementsRoute = instances[6];

                    migrationBuilder.InsertData(
                      table: "PI.Instances",
                      columns: new[] { "Id", "WebId", "PIId", "Name", "Description", "SelfRoute", "ElementsRoute", "DatabaseId" },
                      values: new object[] {
                          instanceId,
                          instanceWebId,
                          instancePIId,
                          instanceName,
                          instanceDescription,
                          instanceSelfRoute,
                          instanceElementsRoute,
                          databaseId
                      });

                    if (instanceId == bravoId)
                    {
                        var intakePressureId = Guid.NewGuid();
                        var pressurePDGId = Guid.NewGuid();
                        var WHPId = Guid.NewGuid();

                        var elementsData = new List<object[]>
                        {
                            new object[] {
                                intakePressureId,
                                "F1EmcaZI8jdsuU6iCfbmKdB6iQA39nlIAA7hGxjwBQVoy5FQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1I",
                                "94677f03-0080-11ee-b18f-0050568cb915",
                                "Intake Pressure ESP Sensor",
                                "",
                                "https://prrjbsrvvm170.petrorio.local/piwebapi/elements/F1EmcaZI8jdsuU6iCfbmKdB6iQA39nlIAA7hGxjwBQVoy5FQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1I",
                                "https://prrjbsrvvm170.petrorio.local/piwebapi/elements/F1EmcaZI8jdsuU6iCfbmKdB6iQA39nlIAA7hGxjwBQVoy5FQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1I/attributes"
                            },
                            new object[] {
                                pressurePDGId,
                                "F1EmcaZI8jdsuU6iCfbmKdB6iQuartfoAA7hGxjwBQVoy5FQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cUFJFU1NVUkUgUERHIDE",
                                "7eedaab9-0080-11ee-b18f-0050568cb915",
                                "Pressure PDG 1",
                                "",
                                "https://prrjbsrvvm170.petrorio.local/piwebapi/elements/F1EmcaZI8jdsuU6iCfbmKdB6iQSna1qIAA7hGxjwBQVoy5FQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cV0hQ",
                                "https://prrjbsrvvm170.petrorio.local/piwebapi/elements/F1EmcaZI8jdsuU6iCfbmKdB6iQuartfoAA7hGxjwBQVoy5FQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cUFJFU1NVUkUgUERHIDE/attributes"
                            },
                            new object[] {
                                WHPId,
                                "F1EmcaZI8jdsuU6iCfbmKdB6iQSna1qIAA7hGxjwBQVoy5FQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cV0hQ",
                                "a8b5764a-0080-11ee-b18f-0050568cb915",
                                "WHP",
                                "",
                                "https://prrjbsrvvm170.petrorio.local/piwebapi/elements/F1EmcaZI8jdsuU6iCfbmKdB6iQOuWSPIvq7RGxjQBQVoy5FQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUE",
                                "https://prrjbsrvvm170.petrorio.local/piwebapi/elements/F1EmcaZI8jdsuU6iCfbmKdB6iQSna1qIAA7hGxjwBQVoy5FQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gQlJBVk9cV0hQ/attributes"
                            },
                        };


                        foreach (var element in elementsData)
                        {
                            var elementId = (Guid)element[0];
                            var elementWebId = element[1];
                            var elementPIId = element[2];
                            var elementName = element[3];
                            var elementDescription = element[4];
                            var elementSelfRoute = element[5];
                            var elementElementsRoute = element[6];

                            migrationBuilder.InsertData(
                              table: "PI.Elements",
                              columns: new[] { "Id", "WebId", "PIId", "Name", "Description", "SelfRoute", "AttributesRoute", "InstanceId" },
                              values: new object[] {
                              elementId,
                              elementWebId,
                              elementPIId,
                              elementName,
                              elementDescription,
                              elementSelfRoute,
                              elementElementsRoute,
                              bravoId
                              });
                        }
                    }
                    else if (instanceId == polvoId)
                    {
                        var intakePressureId = Guid.NewGuid();
                        var vibrationId = Guid.NewGuid();
                        var WHPId = Guid.NewGuid();

                        var elementsData = new List<object[]>
                        {
                            new object[] {
                                intakePressureId,
                                "F1EmcaZI8jdsuU6iCfbmKdB6iQ4610ZoQA7hGxjwBQVoy5FQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1I",
                                "6674ade3-0084-11ee-b18f-0050568cb915",
                                "Intake Pressure ESP Sensor",
                                "",
                                "https://prrjbsrvvm170.petrorio.local/piwebapi/elements/F1EmcaZI8jdsuU6iCfbmKdB6iQ4610ZoQA7hGxjwBQVoy5FQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1I",
                                "https://prrjbsrvvm170.petrorio.local/piwebapi/elements/F1EmcaZI8jdsuU6iCfbmKdB6iQ4610ZoQA7hGxjwBQVoy5FQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcSU5UQUtFIFBSRVNTVVJFIEVTUCBTRU5TT1I/attributes"
                            },
                            new object[] {
                                vibrationId,
                                "F1EmcaZI8jdsuU6iCfbmKdB6iQMmz5wZ9Q7hGxmABQVoy5FQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcVklCUkHDh8ODTw",
                                "c1f96c32-509f-11ee-b198-0050568cb915",
                                "Vibração",
                                "",
                                "https://prrjbsrvvm170.petrorio.local/piwebapi/elements/F1EmcaZI8jdsuU6iCfbmKdB6iQMmz5wZ9Q7hGxmABQVoy5FQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcVklCUkHDh8ODTw",
                                "https://prrjbsrvvm170.petrorio.local/piwebapi/elements/F1EmcaZI8jdsuU6iCfbmKdB6iQMmz5wZ9Q7hGxmABQVoy5FQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcVklCUkHDh8ODTw/attributes"
                            },
                            new object[] {
                                WHPId,
                                "F1EmcaZI8jdsuU6iCfbmKdB6iQio9kboQA7hGxjwBQVoy5FQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcV0hQ",
                                "6e648f8a-0084-11ee-b18f-0050568cb915",
                                "WHP",
                                "",
                                "https://prrjbsrvvm170.petrorio.local/piwebapi/elements/F1EmcaZI8jdsuU6iCfbmKdB6iQio9kboQA7hGxjwBQVoy5FQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcV0hQ",
                                "https://prrjbsrvvm170.petrorio.local/piwebapi/elements/F1EmcaZI8jdsuU6iCfbmKdB6iQio9kboQA7hGxjwBQVoy5FQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXFBPTFZPLUFcV0hQ/attributes"
                            },
                        };

                        foreach (var element in elementsData)
                        {
                            var elementId = (Guid)element[0];
                            var elementWebId = element[1];
                            var elementPIId = element[2];
                            var elementName = element[3];
                            var elementDescription = element[4];
                            var elementSelfRoute = element[5];
                            var elementElementsRoute = element[6];

                            migrationBuilder.InsertData(
                              table: "PI.Elements",
                              columns: new[] { "Id", "WebId", "PIId", "Name", "Description", "SelfRoute", "AttributesRoute", "InstanceId" },
                              values: new object[] {
                              elementId,
                              elementWebId,
                              elementPIId,
                              elementName,
                              elementDescription,
                              elementSelfRoute,
                              elementElementsRoute,
                              polvoId
                              });
                        }
                    }

                }

            }
            else if (instanceKey.ToUpper() == "FORTE")
            {

                var forteId = Guid.NewGuid();

                var instancesData = new List<object[]>
                    {
                        new object[] {
                            forteId,
                            "F1EmcaZI8jdsuU6iCfbmKdB6iQcNNQVYvq7RGxjQBQVoy5FQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEU",
                            "5550d370-ea8b-11ed-b18d-0050568cb915",
                            "FPSO FORTE",
                            "Cálculos para Albacora",
                            "https://prrjbsrvvm170.petrorio.local/piwebapi/elements/F1EmcaZI8jdsuU6iCfbmKdB6iQcNNQVYvq7RGxjQBQVoy5FQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEU",
                            "https://prrjbsrvvm170.petrorio.local/piwebapi/elements/F1EmcaZI8jdsuU6iCfbmKdB6iQcNNQVYvq7RGxjQBQVoy5FQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEU/elements"
                        }
                    };

                foreach (var instances in instancesData)
                {
                    var instanceId = (Guid)instances[0];
                    var instanceWebId = instances[1];
                    var instancePIId = instances[2];
                    var instanceName = instances[3];
                    var instanceDescription = instances[4];
                    var instanceSelfRoute = instances[5];
                    var instanceElementsRoute = instances[6];

                    migrationBuilder.InsertData(
                      table: "PI.Instances",
                      columns: new[] { "Id", "WebId", "PIId", "Name", "Description", "SelfRoute", "ElementsRoute", "DatabaseId" },
                      values: new object[] {
                          instanceId,
                          instanceWebId,
                          instancePIId,
                          instanceName,
                          instanceDescription,
                          instanceSelfRoute,
                          instanceElementsRoute,
                          databaseId
                      });

                    if (instanceId == forteId)
                    {
                        var PDGPressureId = Guid.NewGuid();
                        var TPTPressureId = Guid.NewGuid();
                        var GASLiftId = Guid.NewGuid();

                        var elementsData = new List<object[]>
                        {
                            new object[] {
                                PDGPressureId,
                                "F1EmcaZI8jdsuU6iCfbmKdB6iQPa1Hcuwn7hGxlABQVozG4QUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcUERHIFBSRVNTw4NPIDE",
                                "7247ad3d-27ec-11ee-b194-0050568cc6e1",
                                "PDG Pressão 1",
                                "",
                                "https://prrjbsrvvm170.petrorio.local/piwebapi/elements/F1EmcaZI8jdsuU6iCfbmKdB6iQPa1Hcuwn7hGxlABQVozG4QUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcUERHIFBSRVNTw4NPIDE",
                                "https://prrjbsrvvm170.petrorio.local/piwebapi/elements/F1EmcaZI8jdsuU6iCfbmKdB6iQPa1Hcuwn7hGxlABQVozG4QUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcUERHIFBSRVNTw4NPIDE/attributes"
                            },
                            new object[] {
                                TPTPressureId,
                                "F1EmcaZI8jdsuU6iCfbmKdB6iQRIIVoe8n7hGxlABQVozG4QUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcVFBUIFBSRVNTw4NPIDE",
                                "a1158244-27ef-11ee-b194-0050568cc6e1",
                                "TPT Pressão 1",
                                "",
                                "https://prrjbsrvvm170.petrorio.local/piwebapi/elements/F1EmcaZI8jdsuU6iCfbmKdB6iQRIIVoe8n7hGxlABQVozG4QUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcVFBUIFBSRVNTw4NPIDE",
                                "https://prrjbsrvvm170.petrorio.local/piwebapi/elements/F1EmcaZI8jdsuU6iCfbmKdB6iQRIIVoe8n7hGxlABQVozG4QUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcVFBUIFBSRVNTw4NPIDE/attributes"
                            },
                            new object[] {
                                GASLiftId,
                                "F1EmcaZI8jdsuU6iCfbmKdB6iQx5aXaP8n7hGxlABQVozG4QUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcVkFaw4NPIERFIEfDgVMgTElGVA",
                                "689796c7-27ff-11ee-b194-0050568cc6e1",
                                "Vazão de gás lift",
                                "",
                                "https://prrjbsrvvm170.petrorio.local/piwebapi/elements/F1EmcaZI8jdsuU6iCfbmKdB6iQx5aXaP8n7hGxlABQVozG4QUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcVkFaw4NPIERFIEfDgVMgTElGVA",
                                "https://prrjbsrvvm170.petrorio.local/piwebapi/elements/F1EmcaZI8jdsuU6iCfbmKdB6iQx5aXaP8n7hGxlABQVozG4QUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEVcVkFaw4NPIERFIEfDgVMgTElGVA/attributes"
                            },
                        };

                        foreach (var element in elementsData)
                        {
                            var elementId = (Guid)element[0];
                            var elementWebId = element[1];
                            var elementPIId = element[2];
                            var elementName = element[3];
                            var elementDescription = element[4];
                            var elementSelfRoute = element[5];
                            var elementElementsRoute = element[6];

                            migrationBuilder.InsertData(
                              table: "PI.Elements",
                              columns: new[] { "Id", "WebId", "PIId", "Name", "Description", "SelfRoute", "AttributesRoute", "InstanceId" },
                              values: new object[] {
                              elementId,
                              elementWebId,
                              elementPIId,
                              elementName,
                              elementDescription,
                              elementSelfRoute,
                              elementElementsRoute,
                              forteId
                              });
                        }
                    }
                }

            }
            else if (instanceKey.ToUpper() == "FRADE")
            {
                var fradeId = Guid.NewGuid();

                var instancesData = new List<object[]>
                    {
                        new object[] {
                            fradeId,
                            "F1EmcaZI8jdsuU6iCfbmKdB6iQcNNQVYvq7RGxjQBQVoy5FQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRk9SVEU",
                            "4b541470-ea8b-11ed-b18d-0050568cb915",
                            "FPSO FRADE",
                            "Cálculos para Frade",
                            "https://prrjbsrvvm170.petrorio.local/piwebapi/elements/F1EmcaZI8jdsuU6iCfbmKdB6iQcBRUS4vq7RGxjQBQVoy5FQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREU",
                            "https://prrjbsrvvm170.petrorio.local/piwebapi/elements/F1EmcaZI8jdsuU6iCfbmKdB6iQcBRUS4vq7RGxjQBQVoy5FQUFJSSkJTUlZWTTE3MFxQUklPIC0gQ8OBTENVTE9TXEZQU08gRlJBREU/elements"
                        }
                    };

                foreach (var instances in instancesData)
                {
                    var instanceId = instances[0];
                    var instanceWebId = instances[1];
                    var instancePIId = instances[2];
                    var instanceName = instances[3];
                    var instanceDescription = instances[4];
                    var instanceSelfRoute = instances[5];
                    var instanceElementsRoute = instances[6];

                    migrationBuilder.InsertData(
                      table: "PI.Instances",
                      columns: new[] { "Id", "WebId", "PIId", "Name", "Description", "SelfRoute", "ElementsRoute", "DatabaseId" },
                      values: new object[] {
                          instanceId,
                          instanceWebId,
                          instancePIId,
                          instanceName,
                          instanceDescription,
                          instanceSelfRoute,
                          instanceElementsRoute,
                          databaseId
                      });

                }

            }
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
