namespace Skyline.DataMiner.ServiceNow.Connector.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using global::Skyline.DataMiner.Automation;
    using global::Skyline.DataMiner.Core.DataMinerSystem.Automation;
    using global::Skyline.DataMiner.Core.DataMinerSystem.Common;

    public class Connector
    {
        public enum NamingFormat
        {
            Name,
            Name_Label,
            Label,
            Label_Name,
            NMS_Name,
            NMS_Label,
            NMS_Name_Label,
            NMS_Custom,
            Custom,
        }

        private static Dictionary<string, ConnectorMapping> classMappingsByConnector;

        public static Dictionary<string, ConnectorMapping> Mappings
        {
            get
            {
                if (classMappingsByConnector != null)
                {
                    return classMappingsByConnector;
                }

                classMappingsByConnector = new Dictionary<string, ConnectorMapping>
                {
                    // TODO: Include all supported Connector/CI Types mappings here
                    {
                        "iDirect Platform", new ConnectorMapping(
                            new List<ClassMapping>
                            {
                                new ClassMapping
                                {
                                    Class = "Evolution NMS",
                                    IsParent = true,
                                    NamingFormat = NamingFormat.Name,
                                    AttributesByTableID = new Dictionary<int, List<ClassAttribute>>
                                    {
                                        //  TODO: Add attributes here
                                        //  ...
                                    },
                                },
                                new ClassMapping
                                {
                                    Class = "Evolution Remote",
                                    IsParent = false,
                                    NamingFormat = NamingFormat.Custom,
                                    AttributesByTableID = new Dictionary<int, List<ClassAttribute>>
                                    {
                                        //  TODO: Add attributes here
                                        {
                                            300,
                                            new List<ClassAttribute>
                                            {
                                                new ClassAttribute("pk", 0, false),
                                                new ClassAttribute("u_label", 1, false),
                                                new ClassAttribute("u_status", 6, true),
                                                new ClassAttribute("u_network_id", 9, false),
                                                new ClassAttribute("u_network_name", 10, true),
                                                new ClassAttribute("u_inroute_group_id", 11, false),
                                                new ClassAttribute("u_inroute_group", 12, false),
                                                new ClassAttribute("u_customer_id", 13, false),
                                                new ClassAttribute("u_active_sw_version", 14, false),
                                                new ClassAttribute("u_hw_type", 15, false),
                                                new ClassAttribute("u_protocol_processor", 16, false),
                                                new ClassAttribute("serial_number", 17, false),
                                                new ClassAttribute("u_nms_name", 18, false),
                                            }
                                        },
                                        //{
                                        //    2000,
                                        //    new List<ClassAttribute>
                                        //    {
                                        //        new ClassAttribute("pk", 0),
                                        //    }
                                        //},
                                    },
                                },
                                new ClassMapping
                                {
                                    Class = "Evolution Linecard",
                                    IsParent = false,
                                    NamingFormat = NamingFormat.NMS_Name,
                                    AttributesByTableID = new Dictionary<int, List<ClassAttribute>>
                                    {
                                        //  TODO: Add attributes here
                                        {
                                            400,
                                            new List<ClassAttribute>
                                            {
                                                new ClassAttribute("pk", 0, false),
                                                new ClassAttribute("u_label", 1, false),
                                                new ClassAttribute("u_status", 4, true),
                                                new ClassAttribute("u_inroute_group", 8, false),
                                                new ClassAttribute("u_customer_id", 9, false),
                                                new ClassAttribute("u_active_sw_version", 10, false ),
                                                new ClassAttribute("u_hw_type", 11, false),
                                                new ClassAttribute("u_protocol_processor", 12, false),
                                                new ClassAttribute("serial_number", 13, false),
                                                new ClassAttribute("u_chassis_id", 14, false),
                                                new ClassAttribute("u_chassis_slot_number", 15, false),
                                                new ClassAttribute("u_network_id", 16, false),
                                                new ClassAttribute("u_nms_name", 17, false),
                                            }
                                        },
                                        //{
                                        //    1700,
                                        //    new List<ClassAttribute>
                                        //    {
                                        //        new ClassAttribute("pk", 6),
                                        //    }
                                        //},
                                        //{
                                        //    6400,
                                        //    new List<ClassAttribute>
                                        //    {
                                        //        new ClassAttribute("pk", 0),
                                        //    }
                                        //},
                                        //{
                                        //    15000,
                                        //    new List<ClassAttribute>
                                        //    {
                                        //        new ClassAttribute("pk", 0),
                                        //    }
                                        //},
                                    },
                                },
                                new ClassMapping
                                {
                                    Class = "Evolution Network",
                                    IsParent = false,
                                    NamingFormat = NamingFormat.Name_Label,
                                    AttributesByTableID = new Dictionary<int, List<ClassAttribute>>
                                    {
                                        //  TODO: Add attributes here
                                        {
                                            600,
                                            new List<ClassAttribute>
                                            {
                                                new ClassAttribute("pk", 0, false),
                                                new ClassAttribute("u_label", 1, false),
                                                new ClassAttribute("u_status", 3, true),
                                                new ClassAttribute("u_teleport_id", 6, false),
                                                new ClassAttribute("u_protocol_processor", 7, false),
                                                new ClassAttribute("u_nms_name", 8, false),
                                            }
                                        },
                                        //{
                                        //    6000,
                                        //    new List<ClassAttribute>
                                        //    {
                                        //        new ClassAttribute("pk", 0),
                                        //    }
                                        //},
                                    },
                                },
                                new ClassMapping
                                {
                                    Class = "Evolution Chassis",
                                    IsParent = false,
                                    NamingFormat = NamingFormat.Name_Label,
                                    AttributesByTableID = new Dictionary<int, List<ClassAttribute>>
                                    {
                                        //  TODO: Add attributes here
                                        {
                                            1500,
                                            new List<ClassAttribute>
                                            {
                                                new ClassAttribute("pk", 0, false),
                                                new ClassAttribute("u_label", 1, false),
                                                new ClassAttribute("u_status", 3, true),
                                                new ClassAttribute("u_nms_ip", 4, false),
                                                new ClassAttribute("u_nms_name", 5, false),
                                            }
                                        },
                                        {
                                            1700,
                                            new List<ClassAttribute>
                                            {
                                                new ClassAttribute("fk", 1, false),
                                                new ClassAttribute("u_linecard", 4, false),
                                                new ClassAttribute("u_redundancy_linecard", 8, false),
                                            }
                                        },
                                    },
                                },
                                new ClassMapping
                                {
                                    Class = "Evolution Inroute Group",
                                    IsParent = false,
                                    NamingFormat = NamingFormat.Name_Label,
                                    AttributesByTableID = new Dictionary<int, List<ClassAttribute>>
                                    {
                                        //  TODO: Add attributes here
                                        {
                                            7400,
                                            new List<ClassAttribute>
                                            {
                                                new ClassAttribute("pk", 0, false),
                                                new ClassAttribute("u_label", 1, false),
                                                new ClassAttribute("u_network_id", 2, false),
                                            }
                                        },
                                    },
                                },
                                new ClassMapping
                                {
                                    Class = "Evolution Protocol Processor",
                                    IsParent = false,
                                    NamingFormat = NamingFormat.NMS_Custom,
                                    AttributesByTableID = new Dictionary<int, List<ClassAttribute>>
                                    {
                                        //  TODO: Add attributes here
                                        {
                                            6300,
                                            new List<ClassAttribute>
                                            {
                                                new ClassAttribute("pk", 0, false),
                                                new ClassAttribute("u_label", 1, false),
                                                new ClassAttribute("u_network_id", 2, false),
                                            }
                                        },
                                    },
                                },
                                new ClassMapping
                                {
                                    Class = "Evolution Protocol Processor Blade",
                                    IsParent = false,
                                    NamingFormat = NamingFormat.NMS_Label,
                                    AttributesByTableID = new Dictionary<int, List<ClassAttribute>>
                                    {
                                        //  TODO: Add attributes here
                                        {
                                            6300,
                                            new List<ClassAttribute>
                                            {
                                                new ClassAttribute("pk", 0, false),
                                                new ClassAttribute("u_label", 1, false),
                                                new ClassAttribute("u_network_id", 2, false),
                                                new ClassAttribute("u_tunnel_address", 3, false),
                                                new ClassAttribute("u_tunnel_subnet", 4, false),
                                                new ClassAttribute("u_upstream_address", 5, false),
                                                new ClassAttribute("u_upstream_subnet", 6, false),
                                            }
                                        },
                                    },
                                },
                            },
                            new List<Relationship>
                            {
                                // TODO: Add class relationships here
                                new Relationship("Evolution Remote", "Evolution NMS", "u_nms_name", "Managed by::Manages", false),
                                new Relationship("Evolution Linecard", "Evolution NMS", "u_nms_name", "Managed by::Manages", false),
                                new Relationship("Evolution Network", "Evolution NMS", "u_nms_name", "Managed by::Manages", false),
                                new Relationship("Evolution Inroute Group", "Evolution Remote", "u_inroute_group", "Connected by::Connects", true),
                                new Relationship("Evolution Network", "Evolution Remote", "u_network_name", "Receives data from::Sends data to", true),
                                new Relationship("Evolution Network", "Evolution Linecard", "u_network_id", "Depends on::Used by", true),
                                new Relationship("Evolution Linecard", "Evolution Chassis", "u_chassis_id", "Located in::Houses", false),
                                new Relationship("Evolution Network", "Evolution Protocol Processor", "u_protocol_processor", "Depends on::Used by", false),
                                //TODO: Change relationship mapping as following mapping requires data from different CI Class 
                                new Relationship("Evolution Linecard", "Evolution Linecard", "u_redundancy_linecard", "DR provided by::Provides DR for", false),
                            })
                    },
                    {
                        "Newtec Dialog Time Series Database", new ConnectorMapping(
                            new List<ClassMapping>
                            {
                                new ClassMapping
                                {
                                    Class = "Dialog TSDB NMS",
                                    IsParent = true,
                                    NamingFormat = NamingFormat.Name,
                                    AttributesByTableID = new Dictionary<int, List<ClassAttribute>>
                                    {
                                        //  TODO: Add attributes here
                                    },
                                },
                                new ClassMapping
                                {
                                    Class = "Dialog TSDB Remote",
                                    IsParent = false,
                                    NamingFormat = NamingFormat.Label,
                                    AttributesByTableID = new Dictionary<int, List<ClassAttribute>>
                                    {
                                        //  TODO: Add attributes here
                                        {
                                            12000,
                                            new List<ClassAttribute>
                                            {
                                                new ClassAttribute("pk", 0, false),
                                                new ClassAttribute("u_label", 7, false),
                                                new ClassAttribute("u_modem_name", 1, false),
                                                new ClassAttribute("u_modem_type", 2, false),
                                                new ClassAttribute("u_return_technology", 3, false),
                                                new ClassAttribute("u_mac_address", 4, false),
                                                new ClassAttribute("u_monitoring_type", 5, false),
                                                new ClassAttribute("u_network_name", 6, false),
                                                new ClassAttribute("u_network_config", 8, false),
                                                new ClassAttribute("u_sw_version", 9, false),
                                                new ClassAttribute("u_last_network_config", 10, false),
                                                new ClassAttribute("u_status", 149, true),
                                                new ClassAttribute("u_nms_name", -1, false),
                                            }
                                        },
                                        {
                                            21000,
                                            new List<ClassAttribute>
                                            {
                                                new ClassAttribute("pk", 0, false),
                                                new ClassAttribute("u_label", 1, false),
                                                new ClassAttribute("u_beam_state", 2, true),
                                                new ClassAttribute("u_active_beam", 3, true),
                                                new ClassAttribute("u_switching_beam", 4, false),
                                            }
                                        },
                                    },
                                },
                                new ClassMapping
                                {
                                    Class = "Dialog TSDB Network",
                                    IsParent = false,
                                    NamingFormat = NamingFormat.Label,
                                    AttributesByTableID = new Dictionary<int, List<ClassAttribute>>
                                    {
                                        //  TODO: Add attributes here
                                        {
                                            15000,
                                            new List<ClassAttribute>
                                            {
                                                new ClassAttribute("pk", 0, false),
                                                new ClassAttribute("u_label", 1, false),
                                                new ClassAttribute("u_beam_id", 2, false),
                                                new ClassAttribute("u_beam_signaled_name", 3, false),
                                                new ClassAttribute("u_beam_orbital_position", 4, false),
                                                new ClassAttribute("u_beam_east_west_flag", 5, false),
                                            }
                                        },
                                    },
                                },
                            },
                            new List<Relationship>
                            {
                                // TODO: Add class relationships here
                                new Relationship("Dialog TSDB Remote", "Dialog TSDB NMS", "u_nms_name", "Managed by::Manages", false),
                                new Relationship("Dialog TSDB Network", "Dialog TSDB NMS", "u_nms_name", "Managed by::Manages", false),
                                new Relationship("Dialog TSDB Network", "Dialog TSDB Remote", "u_active_beam", "Receives data from::Sends data to", true),
                            })
                    },
                };

                return classMappingsByConnector;
            }
        }

        public static List<Element> GetSupportedDmsElements(IEngine engine)
        {
            var elements = new List<Element>();

            var supportedConnectors = Mappings.Keys.ToList();

            foreach (var connector in supportedConnectors)
            {
                var foundElements = engine.FindElementsByProtocol(connector);

                if (foundElements.Length > 0)
                {
                    elements.AddRange(foundElements);
                }
            }

            return elements;
        }

        public static List<ParameterDetails> GetPushParameterDetailsByConnector(string protocolName)
        {
            if (Mappings.ContainsKey(protocolName))
            {
                var parameterUpdates = new List<ParameterDetails>();

                var connectorMapping = Mappings[protocolName];

                var classAttributesByTablePID = connectorMapping.ClassMappings
                    .SelectMany(x => x.AttributesByTableID)
                    .GroupBy(x => x.Key)
                    .ToDictionary(g => g.Key, g => g.SelectMany(kvp => kvp.Value).ToList());

                foreach (var attributeKvp in classAttributesByTablePID)
                {
                    var pushAttributes = attributeKvp.Value.Where(x => x.HasPushEvent).ToList();

                    if (pushAttributes.Count == 0) continue;

                    var parameterDetails = pushAttributes
                        .Select(attribute => new ParameterDetails(attribute.Name, new KeyValuePair<int, int>(attributeKvp.Key, attribute.ColumnIdx)));

                    parameterUpdates.AddRange(parameterDetails);
                }

                return parameterUpdates;
            }
            else
            {
                throw new ArgumentException($"Protocol name '{protocolName}' could not be found in connector mappings.");
            }
        }
    }

    public class ConnectorMapping
    {
        public List<ClassMapping> ClassMappings { get; set; }

        public List<Relationship> Relationships { get; set; }

        public ConnectorMapping(List<ClassMapping> classMappings, List<Relationship> relationships)
        {
            ClassMappings = classMappings;
            Relationships = relationships;
        }
    }

    public class ClassMapping
    {
        private Dictionary<string, Func<Engine, List<Property>, string, string>> ciUniqueIdFunctionMapper;

        public Dictionary<string, Func<Engine, List<Property>, string, string>> CiUniqueIdFunctionMapper
        {
            get
            {
                if (ciUniqueIdFunctionMapper != null)
                {
                    return ciUniqueIdFunctionMapper;
                }

                ciUniqueIdFunctionMapper = new Dictionary<string, Func<Engine, List<Property>, string, string>>
                {
                    //  TODO: Add methods used to build CIs using custom methods
                    { "Evolution Remote", GetEvolutionRemoteUniqueID },
                    { "Evolution Protocol Processor", GetEvolutionProtocolProcessorUniqueID },
                };

                return ciUniqueIdFunctionMapper;
            }
        }

        public string Class { get; set; }

        public bool IsParent { get; set; }

        public Connector.NamingFormat NamingFormat { get; set; }

        public Dictionary<int, List<ClassAttribute>> AttributesByTableID { get; set; }

        public Dictionary<string, List<Property>> GetPropertiesByCiUniqueID(IEngine engine, Element element)
        {
            var propertiesByPK = new Dictionary<string, List<Property>>();
            var propertiesByFK = new Dictionary<string, Dictionary<string, List<string>>>();

            var rowsByTableID = AttributesByTableID.ToDictionary(x => x.Key, x => GetClassCiRows(engine, element, x.Key));

            foreach (var rowByTableKvp in rowsByTableID)
            {
                if (rowsByTableID.Values.Count == 0) continue;

                ParseRowProperties(engine, propertiesByPK, propertiesByFK, rowByTableKvp);
            }

            var propertiesByUniqueID = new Dictionary<string, List<Property>>();

            string elementDmsID = element.DmaId + "/" + element.ElementId;

            foreach (var item in propertiesByPK)
            {
                var uniqueID = GetCiRowUniqueID(engine, item.Key, item.Value, elementDmsID);

                if (String.IsNullOrWhiteSpace(uniqueID))
                {
                    string message = "GetPropertiesByCiUniqueID| Could not retrieve unique ID for key " + item.Key + " and element '" + element.ElementName + "'.";
                    engine.GenerateInformation(message);
                    continue;
                }

                var propertyList = item.Value;

                if (propertiesByUniqueID.ContainsKey(uniqueID)) continue;

                propertyList.Add(new Property("name", uniqueID));
                propertyList.Add(new Property("operational_status", "Operational"));

                propertiesByUniqueID.Add(uniqueID, propertyList);
            }

            // TODO: Include logic to parse properties with multiple values (one to many)
            //
            //foreach (var item in propertiesByFK)
            //{
            //    var uniqueID = item.Key;
            //    var propertyValuesByName = item.Value;
            //}

            return propertiesByUniqueID;
        }

        private static List<object[]> GetClassCiRows(IEngine engine, Element element, int tablePid)
        {
            try
            {
                var dms = engine.GetDms();

                var dmsElement = dms.GetElement(new DmsElementId(element.DmaId, element.ElementId));

                var dmsTable = dmsElement.GetTable(tablePid);

                var table = dmsTable.GetRows();

                return GetRowList(table);
            }
            catch (ElementStoppedException)
            {
                engine.GenerateInformation("GetClassCiRows| Element '" + element.Name + "' is stopped.");
                return new List<object[]>();
            }
            catch (Exception ex)
            {
                engine.Log("GetClassCiRows| Exception:\n\n" + ex + "\n\n");
                return new List<object[]>();
            }
        }

        private static List<object[]> GetRowList(object[][] table)
        {
            var rowList = new List<object[]>();

            for (int i = 0; i < table.Length; i++)
            {
                rowList.Add(table[i]);
            }

            return rowList;
        }

        private void ParseRowProperties(IEngine engine, Dictionary<string, List<Property>> propertiesByPK, Dictionary<string, Dictionary<string, List<string>>> propertiesByFK, KeyValuePair<int, List<object[]>> rowByTableKvp)
        {
            int tablePid = rowByTableKvp.Key;
            var rows = rowByTableKvp.Value;

            var primaryKeyAttribute = AttributesByTableID[tablePid].FirstOrDefault(x => x.Name.Equals("pk"));
            var foreignKeyAttribute = AttributesByTableID[tablePid].FirstOrDefault(x => x.Name.Equals("fk"));

            foreach (var row in rows)
            {
                if (primaryKeyAttribute != null)
                {
                    ParsePropertiesByRowPK(engine, propertiesByPK, tablePid, primaryKeyAttribute, row);
                }

                if (foreignKeyAttribute != null)
                {
                    var fk = Convert.ToString(row[foreignKeyAttribute.ColumnIdx]);

                    if (String.IsNullOrWhiteSpace(fk) || fk.Equals("-1") || fk.Equals("NA")) continue;

                    if (!propertiesByFK.ContainsKey(fk))
                    {
                        propertiesByFK.Add(fk, new Dictionary<string, List<string>>());
                    }

                    ParsePropertiesByRowFK(propertiesByFK[fk], row, tablePid);
                }
            }
        }

        private void ParsePropertiesByRowPK(IEngine engine, Dictionary<string, List<Property>> propertiesByPK, int tablePid, ClassAttribute primaryKeyAttribute, object[] row)
        {
            var pk = Convert.ToString(row[primaryKeyAttribute.ColumnIdx]);

            if (String.IsNullOrWhiteSpace(pk) || pk.Equals("-1") || pk.Equals("NA")) return;

            if (!propertiesByPK.ContainsKey(pk))
            {
                propertiesByPK.Add(pk, new List<Property>());
            }

            foreach (var classAttribute in AttributesByTableID[tablePid])
            {
                if (classAttribute.Name.Equals("pk")) continue;

                propertiesByPK[pk].Add(new Property(classAttribute.Name, Convert.ToString(row[classAttribute.ColumnIdx])));
            }
        }

        private void ParsePropertiesByRowFK(Dictionary<string, List<string>> propertiesValuesByName, object[] row, int tablePid)
        {
            foreach (var classAttribute in AttributesByTableID[tablePid])
            {
                if (classAttribute.Name.Equals("fk")) continue;

                if (propertiesValuesByName.ContainsKey(classAttribute.Name))
                {
                    propertiesValuesByName[classAttribute.Name].Add(Convert.ToString(row[classAttribute.ColumnIdx]));
                }
                else
                {
                    propertiesValuesByName.Add(classAttribute.Name, new List<string> { Convert.ToString(row[classAttribute.ColumnIdx]) });
                }
            }
        }

        private string GetCiRowUniqueID(IEngine engine, string pk, List<Property> properties, string elementName)
        {
            switch (NamingFormat)
            {
                case Connector.NamingFormat.Name:
                    {
                        return pk;
                    }

                case Connector.NamingFormat.Name_Label:
                    {
                        var labelProperty = properties.FirstOrDefault(x => x.Name.Equals("u_label"));

                        return labelProperty != null ? pk + "_" + labelProperty.Value : String.Empty;
                    }

                case Connector.NamingFormat.Label:
                    {
                        var labelProperty = properties.FirstOrDefault(x => x.Name.Equals("u_label"));

                        return labelProperty != null ? labelProperty.Value : String.Empty;
                    }

                case Connector.NamingFormat.Label_Name:
                    {
                        var labelProperty = properties.FirstOrDefault(x => x.Name.Equals("u_label"));

                        return labelProperty != null ? labelProperty.Value + "_" + pk : String.Empty;
                    }

                case Connector.NamingFormat.NMS_Name:
                    {
                        return elementName + "_" + pk;
                    }

                case Connector.NamingFormat.NMS_Label:
                    {
                        var labelProperty = properties.FirstOrDefault(x => x.Name.Equals("u_label"));

                        return labelProperty != null ? elementName + "_" + labelProperty.Value : String.Empty;
                    }

                case Connector.NamingFormat.NMS_Name_Label:
                    {
                        var labelProperty = properties.FirstOrDefault(x => x.Name.Equals("u_label"));

                        return labelProperty != null ? elementName + "_" + pk + "_" + labelProperty.Value : String.Empty;
                    }

                case Connector.NamingFormat.NMS_Custom:
                    {
                        return elementName + "_" + CiUniqueIdFunctionMapper[Class].Invoke((Engine)engine, properties, pk);
                    }

                case Connector.NamingFormat.Custom:
                    {
                        return CiUniqueIdFunctionMapper[Class].Invoke((Engine)engine, properties, pk);
                    }

                default:
                    return String.Empty;
            }
        }

        public static string GetEvolutionRemoteUniqueID(Engine engine, List<Property> properties, string pk)
        {
            var labelProperty = properties.FirstOrDefault(x => x.Name.Equals("u_label"));

            var customerIdProperty = properties.FirstOrDefault(x => x.Name.Equals("u_customer_id"));

            return labelProperty != null && customerIdProperty != null ? customerIdProperty.Value + "_" + pk + "_" + labelProperty.Value : String.Empty;
        }

        public static string GetEvolutionProtocolProcessorUniqueID(Engine engine, List<Property> properties, string pk)
        {
            string uniqueID = "PP Controller " + pk.Split('_').First();

            var labelProperty = properties.FirstOrDefault(x => x.Name.Equals("u_label"));

            if (labelProperty != null)
            {
                labelProperty.Value = uniqueID;
            }

            return uniqueID;
        }

        public static void FillTableParameterValues(Dictionary<string, List<ParameterDetails>> parameterValuesByPK, List<ParameterDetails> pushParameters, List<uint> parameterIndices, Dictionary<string, object[]> rowsByPK)
        {
            foreach (var rowKvp in rowsByPK)
            {
                if (!parameterValuesByPK.ContainsKey(rowKvp.Key))
                {
                    parameterValuesByPK.Add(rowKvp.Key, new List<ParameterDetails>());
                }

                foreach (var parameter in pushParameters)
                {
                    var searchedParameter = parameterValuesByPK[rowKvp.Key].FirstOrDefault(p => p.AttributeName.Equals(parameter.AttributeName));

                    if (searchedParameter == null)
                    {
                        searchedParameter = new ParameterDetails(parameter.AttributeName, parameter.ParameterIdxByPid);

                        parameterValuesByPK[rowKvp.Key].Add(searchedParameter);
                    }

                    if (parameter.ParameterIdxByPid.Value == -1) continue;

                    int rowIdx = parameterIndices.FindIndex(x => x == (uint)parameter.ParameterIdxByPid.Value);

                    if (rowIdx == -1) continue;

                    searchedParameter.CurrentValue = Convert.ToString(rowKvp.Value[rowIdx]);
                }
            }
        }

        public static void RemoveDeprecatedParameterValueKeys(Dictionary<string, List<ParameterDetails>> parameterValuesByPK, Dictionary<string, object[]> rowsByPK)
        {
            var deprecatedPKs = parameterValuesByPK.Keys.Where(pk => !rowsByPK.ContainsKey(pk)).ToList();

            foreach (var deprecatedPK in deprecatedPKs)
            {
                parameterValuesByPK.Remove(deprecatedPK);
            }
        }

        public static void RemoveDeprecatedAttributes(Dictionary<string, List<ParameterDetails>> parameterValuesByPK, List<ParameterDetails> pushParameters)
        {
            var pushParameterNames = pushParameters.Select(p => p.AttributeName).ToList();

            foreach (var parameterValuesKvp in parameterValuesByPK)
            {
                parameterValuesKvp.Value.RemoveAll(p => !pushParameterNames.Contains(p.AttributeName));
            }
        }

        public static bool TryGetParameterUpdatesToSend(Dictionary<string, List<ParameterDetails>> parameterValuesByPK, out Dictionary<string, List<ParameterDetails>> parameterUpdates)
        {
            parameterUpdates = new Dictionary<string, List<ParameterDetails>>();

            foreach (var parameterDetailsKvp in parameterValuesByPK)
            {
                foreach (var parameter in parameterDetailsKvp.Value)
                {
                    if (!parameter.CurrentValue.Equals(parameter.PreviousValue))
                    {
                        if (parameterUpdates.ContainsKey(parameterDetailsKvp.Key))
                        {
                            parameterUpdates[parameterDetailsKvp.Key].Add(new ParameterDetails(parameter.AttributeName, parameter.CurrentValue));
                        }
                        else
                        {
                            parameterUpdates.Add(parameterDetailsKvp.Key, new List<ParameterDetails>
                        {
                            new ParameterDetails(parameter.AttributeName, parameter.CurrentValue),
                        });
                        }
                    }

                    //Clear parameter values for next polling cycle
                    parameter.PreviousValue = parameter.CurrentValue;
                    parameter.CurrentValue = String.Empty;
                }
            }

            return parameterUpdates.Count > 0;
        }
    }

    public class ClassAttribute
    {
        public string Name { get; set; }

        public int ColumnIdx { get; set; }

        public bool HasPushEvent { get; set; }

        public ClassAttribute(string name, int columnIdx, bool hasPushEvent)
        {
            Name = name;
            ColumnIdx = columnIdx;
            HasPushEvent = hasPushEvent;
        }
    }

    public class Property
    {
        public enum State
        {
            Unknown = -99,
            Deactivated = 0,
            Active,
            Removed,
            Decomissioned,
        }

        public string Name { get; set; }

        public string Value { get; set; }

        public Property(string name, string value)
        {
            Name = name;
            Value = GetPropertyValueByName(value);
        }

        private string GetPropertyValueByName(string value)
        {
            switch (Name)
            {
                case "u_status":
                    return GetInstanceStatus(value);
                default:
                    return value;
            }
        }

        public static string GetInstanceStatus(string status)
        {
            if (String.IsNullOrWhiteSpace(status) || status.Equals("True"))
            {
                return State.Active.ToString();
            }

            return !IsNumerical(status) ? status : GetEnumValue(status).ToString();
        }

        private static State GetEnumValue(string input)
        {
            int index;
            if (int.TryParse(input, out index) && Enum.IsDefined(typeof(State), index))
            {
                return (State)index;
            }
            else
            {
                return State.Unknown;
            }
        }

        private static bool IsNumerical(string input)
        {
            // Attempt to parse the input string as a double
            if (Int32.TryParse(input.Trim(), out _))
            {
                return true; // Input is numerical
            }
            else
            {
                return false; // Input is not numerical
            }
        }
    }

    public class ParameterDetails
    {
        public string AttributeName { get; set; }

        public KeyValuePair<int, int> ParameterIdxByPid { get; set; }

        public string CurrentValue { get; set; }

        public string PreviousValue { get; set; }

        public ParameterDetails(string attributeName, KeyValuePair<int, int> paramIdxByPid)
        {
            AttributeName = attributeName;
            ParameterIdxByPid = paramIdxByPid;
            CurrentValue = String.Empty;
            PreviousValue = String.Empty;
        }

        public ParameterDetails(string attributeName, string currentValue)
        {
            AttributeName = attributeName;
            ParameterIdxByPid = new KeyValuePair<int, int>();
            CurrentValue = currentValue;
            PreviousValue = String.Empty;
        }
    }

    public class Relationship
    {
        public string Name { get; set; }

        public string ParentClass { get; set; }

        public string ChildClass { get; set; }

        public string LinkedProperty { get; set; }

        public string Type { get; set; }

        public bool IsMappedFromParent { get; set; }

        public Relationship(string childClass, string parentClass, string relationshipProperty, string type, bool isMappedFromParent)
        {
            Name = childClass + "/" + type + "/" + parentClass;
            ParentClass = parentClass;
            ChildClass = childClass;
            LinkedProperty = relationshipProperty;
            Type = type;
            IsMappedFromParent = isMappedFromParent;
        }
    }
}
