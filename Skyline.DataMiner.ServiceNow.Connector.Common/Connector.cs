namespace Skyline.DataMiner.ServiceNow.Connector.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using global::Skyline.DataMiner.Automation;
    using global::Skyline.DataMiner.Core.DataMinerSystem.Automation;
    using global::Skyline.DataMiner.Core.DataMinerSystem.Common;
    using SLDataGateway.API.Tasks.Upsert;
    using static Skyline.DataMiner.ServiceNow.Connector.Common.Connector;

    public class Connector
    {
        public enum NamingFormat
        {
            // Naming format always includes parent element name to avoid duplicating unique IDs (for instance in case there are duplicate elements)
            Name,
            Name_Label,
            Label,
            Label_Name,
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
                                    TargetTable = "u_cmdb_ci_appl_nms_evolution",
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
                                    TargetTable = "u_cmdb_ci_modem_evolution_remote",
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
                                    },
                                },
                                new ClassMapping
                                {
                                    Class = "Evolution Linecard",
                                    TargetTable = "u_cmdb_ci_modem_evolution_linecard",
                                    IsParent = false,
                                    NamingFormat = NamingFormat.Name,
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
                                    },
                                },
                                new ClassMapping
                                {
                                    Class = "Evolution Network",
                                    TargetTable = "u_cmdb_ci_group_evolution_network",
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
                                                //TODO: Include Network PP Name ???
                                            }
                                        },
                                        {
                                            6000,
                                            new List<ClassAttribute>
                                            {
                                                new ClassAttribute("pk", 0, false),
                                                new ClassAttribute("u_network_pp_name", 30, false),
                                            }
                                        },
                                    },
                                },
                                new ClassMapping
                                {
                                    Class = "Evolution Chassis",
                                    TargetTable = "u_cmdb_ci_evolution_chassis",
                                    IsParent = false,
                                    NamingFormat = NamingFormat.Custom,
                                    AttributesByTableID = new Dictionary<int, List<ClassAttribute>>
                                    {
                                        //  TODO: Add attributes here
                                        {
                                            1500,
                                            new List<ClassAttribute>
                                            {
                                                new ClassAttribute("pk", 0, false),
                                                new ClassAttribute("u_label", 1, false),
                                                new ClassAttribute("u_serial_number", 2, false),
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
                                    TargetTable = "u_cmdb_ci_evolution_inroute_group",
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
                                    TargetTable = "u_cmdb_ci_appl_evolution_pp",
                                    IsParent = false,
                                    NamingFormat = NamingFormat.Custom,
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
                                    TargetTable = "u_cmdb_ci_evolution_protocol_processor_blade",
                                    IsParent = false,
                                    NamingFormat = NamingFormat.Name_Label,
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
                                //new ClassMapping
                                //{
                                //    Class = "Evolution Teleport",
                                //    TargetTable = "u_cmdb_ci_group_evolution_teleport",
                                //    IsParent = false,
                                //    NamingFormat = NamingFormat.Name_Label,
                                //    AttributesByTableID = new Dictionary<int, List<ClassAttribute>>
                                //    {
                                //        //  TODO: Add attributes here
                                //        {
                                //            -1,
                                //            new List<ClassAttribute>
                                //            {

                                //            }
                                //        },
     
                                //    },
                                //},
                                //new ClassMapping
                                //{
                                //    Class = "Evolution Application",
                                //    TargetTable = "u_cmdb_ci_appl_evolution",
                                //    IsParent = false,
                                //    NamingFormat = NamingFormat.Name_Label,
                                //    AttributesByTableID = new Dictionary<int, List<ClassAttribute>>
                                //    {
                                //        //  TODO: Add attributes here
                                //        {
                                //            -1,
                                //            new List<ClassAttribute>
                                //            {

                                //            }
                                //        },
     
                                //    },
                                //},
                                //new ClassMapping
                                //{
                                //    Class = "Evolution Encapsulator",
                                //    TargetTable = "u_cmdb_ci_appl_evolution_encapsulator",
                                //    IsParent = false,
                                //    NamingFormat = NamingFormat.Name_Label,
                                //    AttributesByTableID = new Dictionary<int, List<ClassAttribute>>
                                //    {
                                //        //  TODO: Add attributes here
                                //        {
                                //            -1,
                                //            new List<ClassAttribute>
                                //            {

                                //            }
                                //        },
     
                                //    },
                                //},
                                //new ClassMapping
                                //{
                                //    Class = "Evolution Processing Node",
                                //    TargetTable = "u_cmdb_ci_appl_evolution_processing_node",
                                //    IsParent = false,
                                //    NamingFormat = NamingFormat.Name_Label,
                                //    AttributesByTableID = new Dictionary<int, List<ClassAttribute>>
                                //    {
                                //        //  TODO: Add attributes here
                                //        {
                                //            -1,
                                //            new List<ClassAttribute>
                                //            {

                                //            }
                                //        },
     
                                //    },
                                //},
                            },
                            new List<Relationship>
                            {
                                // TODO: Add class relationships here
                                new Relationship("Evolution Remote", "Evolution NMS", "u_nms_name", String.Empty, String.Empty, "Managed by::Manages", false),
                                new Relationship("Evolution Linecard", "Evolution NMS", "u_nms_name", String.Empty, String.Empty, "Managed by::Manages", false),
                                new Relationship("Evolution Network", "Evolution NMS", "u_nms_name", String.Empty, String.Empty, "Managed by::Manages", false),
                                new Relationship("Evolution Inroute Group", "Evolution Remote", "u_inroute_group", String.Empty, String.Empty, "Connected by::Connects", true),
                                new Relationship("Evolution Network", "Evolution Remote", "u_network_name", String.Empty, String.Empty, "Receives data from::Sends data to", true),
                                new Relationship("Evolution Network", "Evolution Linecard", "u_network_id", String.Empty, String.Empty, "Depends on::Used by", true),
                                new Relationship("Evolution Linecard", "Evolution Chassis", "u_chassis_id", String.Empty, String.Empty, "Located in::Houses", false),
                                new Relationship("Evolution Network", "Evolution Protocol Processor", "u_protocol_processor", String.Empty, String.Empty, "Depends on::Used by", false),
                                //TODO: Change relationship mapping as following mapping requires data from different CI Class 
                                new Relationship("Evolution Linecard", "Evolution Linecard", "u_label", "u_redundancy_linecard", "Evolution Chassis", "DR provided by::Provides DR for", false),
                            })
                    },
                    {
                        "Newtec Dialog Time Series Database", new ConnectorMapping(
                            new List<ClassMapping>
                            {
                                new ClassMapping
                                {
                                    Class = "Dialog NMS",
                                    TargetTable = "u_cmdb_ci_appl_nms_dialog",
                                    IsParent = true,
                                    NamingFormat = NamingFormat.Name,
                                    AttributesByTableID = new Dictionary<int, List<ClassAttribute>>
                                    {
                                        //  TODO: Add attributes here
                                    },
                                },
                                new ClassMapping
                                {
                                    Class = "Dialog Remote",
                                    TargetTable = "u_cmdb_ci_modem_dialog_remote",
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
                                    Class = "Dialog Satellite Network",
                                    TargetTable = "u_cmdb_ci_dialog_satellite_network",
                                    IsParent = false,
                                    NamingFormat = NamingFormat.Name_Label,
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
                                //new ClassMapping
                                //{
                                //    Class = "Dialog Application",
                                //    TargetTable = "u_cmdb_ci_dialog_application",
                                //    IsParent = false,
                                //    NamingFormat = NamingFormat.Name_Label,
                                //    AttributesByTableID = new Dictionary<int, List<ClassAttribute>>
                                //    {
                                //        //  TODO: Add attributes here
                                //        {
                                //            -1,
                                //            new List<ClassAttribute>
                                //            {

                                //            }
                                //        },
     
                                //    },
                                //},
                                //new ClassMapping
                                //{
                                //    Class = "Dialog Hub",
                                //    TargetTable = "u_cmdb_ci_dialog_hub",
                                //    IsParent = false,
                                //    NamingFormat = NamingFormat.Name_Label,
                                //    AttributesByTableID = new Dictionary<int, List<ClassAttribute>>
                                //    {
                                //        //  TODO: Add attributes here
                                //        {
                                //            -1,
                                //            new List<ClassAttribute>
                                //            {

                                //            }
                                //        },
     
                                //    },
                                //},
                                //new ClassMapping
                                //{
                                //    Class = "Dialog Modulator",
                                //    TargetTable = "u_cmdb_ci_dialog_modulator",
                                //    IsParent = false,
                                //    NamingFormat = NamingFormat.Name_Label,
                                //    AttributesByTableID = new Dictionary<int, List<ClassAttribute>>
                                //    {
                                //        //  TODO: Add attributes here
                                //        {
                                //            -1,
                                //            new List<ClassAttribute>
                                //            {

                                //            }
                                //        },
     
                                //    },
                                //},
                                //new ClassMapping
                                //{
                                //    Class = "Dialog Demodulator",
                                //    TargetTable = "u_cmdb_ci_dialog_demodulator",
                                //    IsParent = false,
                                //    NamingFormat = NamingFormat.Name_Label,
                                //    AttributesByTableID = new Dictionary<int, List<ClassAttribute>>
                                //    {
                                //        //  TODO: Add attributes here
                                //        {
                                //            -1,
                                //            new List<ClassAttribute>
                                //            {

                                //            }
                                //        },
     
                                //    },
                                //},
                            },
                            new List<Relationship>
                            {
                                // TODO: Add class relationships here
                                new Relationship("Dialog Remote", "Dialog NMS", "u_nms_name", String.Empty, String.Empty, "Managed by::Manages", false),
                                new Relationship("Dialog Satellite Network", "Dialog NMS", "u_nms_name", String.Empty, String.Empty, "Managed by::Manages", false),
                                new Relationship("Dialog Satellite Network", "Dialog Remote", "u_active_beam", String.Empty, String.Empty, "Receives data from::Sends data to", true),
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

        /// <summary>
        /// Method used to retrieve the protocol attribute values to be pushed into the ServiceNow integration.
        /// </summary>
        /// <param name="protocolName"></param>
        /// <returns>List of parameter updates.</returns>
        /// <exception cref="ArgumentException"></exception>
        public static List<ParameterDetails> GetPushParameterDetailsByConnector(string protocolName)
        {
            if (Mappings.ContainsKey(protocolName))
            {
                var parameterUpdates = new List<ParameterDetails>();

                var connectorMapping = Mappings[protocolName];

                foreach (var classMapping in connectorMapping.ClassMappings)
                {
                    var classAttributesByTablePID = classMapping.AttributesByTableID;

                    foreach (var attributeKvp in classAttributesByTablePID)
                    {
                        var pushAttributes = attributeKvp.Value.Where(x => x.HasPushEvent).ToList();

                        if (pushAttributes.Count == 0) continue;

                        var namingAttributes = GetNamingAttibutes(classMapping.NamingFormat);

                        foreach (var namingAttribute in namingAttributes)
                        {
                            var attributeToAdd = pushAttributes.FirstOrDefault(x => x.Name.Equals(namingAttribute));

                            if (attributeToAdd != null)
                            {
                                // Add class attributes that will be used to retrieve instances Unique IDs
                                pushAttributes.Add(attributeToAdd);
                            }
                        }

                        var parameterDetails = pushAttributes
                            .Select(attribute => new ParameterDetails(attribute.Name, classMapping.Class, new KeyValuePair<int, int>(attributeKvp.Key, attribute.ColumnIdx)));

                        parameterUpdates.AddRange(parameterDetails);
                    }
                }

                return parameterUpdates;
            }
            else
            {
                throw new ArgumentException($"Protocol name '{protocolName}' could not be found in connector mappings.");
            }
        }

        public static List<string> GetNamingAttibutes(NamingFormat classNamingFormat)
        {
            switch (classNamingFormat)
            {
                case NamingFormat.Name_Label:
                case NamingFormat.Label:
                case NamingFormat.Label_Name:
                    {
                        return new List<string> { "u_label" };
                    }

                //case NamingFormat.Custom:

                default:
                    return new List<string>();
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
                    { "Evolution Chassis", GetEvolutionChassisUniqueID },
                    { "Evolution Protocol Processor", GetEvolutionProtocolProcessorUniqueID },
                };

                return ciUniqueIdFunctionMapper;
            }
        }

        public string Class { get; set; }

        public string TargetTable { get; set; }

        public bool IsParent { get; set; }

        public NamingFormat NamingFormat { get; set; }

        public Dictionary<int, List<ClassAttribute>> AttributesByTableID { get; set; }

        /// <summary>
        /// Method used to retrieve parsed property values organized by instance unique ID.
        /// </summary>
        /// <param name="engine"></param>
        /// <param name="element"></param>
        /// <returns>Dictionary containg a list of properties objects by unique id.</returns>
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

            // TODO: Validate logic to parse properties with multiple values (one to many)

            foreach (var item in propertiesByFK)
            {
                var uniqueID = item.Key;
                var propertyValuesByName = item.Value;

                if (!propertiesByUniqueID.ContainsKey(uniqueID))
                {
                    propertiesByUniqueID.Add(uniqueID, new List<Property>());
                }

                var propertyList = propertyValuesByName.Select(kvp => new Property(kvp.Key, String.Join(";", kvp.Value))).ToList();

                propertiesByUniqueID[uniqueID].AddRange(propertyList);
            }

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

                    ParsePropertiesByRowFK(engine, propertiesByFK[fk], row, tablePid);
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

        private void ParsePropertiesByRowFK(IEngine engine, Dictionary<string, List<string>> propertiesValuesByName, object[] row, int tablePid)
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
                        searchedParameter = new ParameterDetails(parameter.AttributeName, parameter.Class, parameter.ParameterIdxByPid);

                        parameterValuesByPK[rowKvp.Key].Add(searchedParameter);
                    }

                    if (parameter.ParameterIdxByPid.Value == -1) continue;

                    int rowIdx = parameterIndices.FindIndex(x => x == (uint)parameter.ParameterIdxByPid.Value);

                    if (rowIdx == -1) continue;

                    searchedParameter.CurrentValue = Convert.ToString(rowKvp.Value[rowIdx]);
                }
            }
        }

        public static void RemoveDeprecatedParameterValueKeys(Dictionary<string, List<ParameterDetails>> parameterValuesByPK, List<string> currentPKs)
        {
            var deprecatedPKs = parameterValuesByPK.Keys.Where(pk => !currentPKs.Contains(pk)).ToList();

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
                            parameterUpdates[parameterDetailsKvp.Key]
                                .Add(new ParameterDetails(parameter.AttributeName, parameter.Class, parameter.CurrentValue));
                        }
                        else
                        {
                            parameterUpdates.Add(parameterDetailsKvp.Key, new List<ParameterDetails>
                            {
                                new ParameterDetails(parameter.AttributeName, parameter.Class, parameter.CurrentValue),
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

        public string GetInstanceUniqueID(IEngine engine, string parentElementName, string pk, NamingFormat classNamingFormat, List<ParameterDetails> parameterDetails)
        {
            switch (classNamingFormat)
            {
                case NamingFormat.Name:
                    {
                        return parentElementName + "_" + pk;
                    }

                case NamingFormat.Name_Label:
                    {
                        var labelAttribute = parameterDetails.FirstOrDefault(x => x.AttributeName.Equals("u_label"));

                        return labelAttribute != null ? parentElementName + "_" + pk + "_" + labelAttribute.CurrentValue : String.Empty;
                    }

                case NamingFormat.Label:
                    {
                        var labelAttribute = parameterDetails.FirstOrDefault(x => x.AttributeName.Equals("u_label"));

                        return labelAttribute != null ? parentElementName + "_" + labelAttribute.CurrentValue : String.Empty;
                    }

                case NamingFormat.Label_Name:
                    {
                        var labelAttribute = parameterDetails.FirstOrDefault(x => x.AttributeName.Equals("u_label"));

                        return labelAttribute != null ? parentElementName + "_" + labelAttribute.CurrentValue + "_" + pk : String.Empty;
                    }

                case NamingFormat.Custom:
                    {
                        var properties = parameterDetails.Select(p => new Property(p.AttributeName, p.CurrentValue)).ToList();

                        return parentElementName + "_" + CiUniqueIdFunctionMapper[Class].Invoke((Engine)engine, properties, pk);
                    }

                default:
                    return String.Empty;
            }
        }

        private string GetCiRowUniqueID(IEngine engine, string pk, List<Property> properties, string parentElementName)
        {
            switch (NamingFormat)
            {
                case NamingFormat.Name:
                    {
                        return parentElementName + "_" + pk;
                    }

                case NamingFormat.Name_Label:
                    {
                        var labelProperty = properties.FirstOrDefault(x => x.Name.Equals("u_label"));

                        return labelProperty != null ? parentElementName + "_" + pk + "_" + labelProperty.Value : String.Empty;
                    }

                case NamingFormat.Label:
                    {
                        var labelProperty = properties.FirstOrDefault(x => x.Name.Equals("u_label"));

                        return labelProperty != null ? parentElementName + "_" + labelProperty.Value : String.Empty;
                    }

                case NamingFormat.Label_Name:
                    {
                        var labelProperty = properties.FirstOrDefault(x => x.Name.Equals("u_label"));

                        return labelProperty != null ? parentElementName + "_" + labelProperty.Value + "_" + pk : String.Empty;
                    }

                case NamingFormat.Custom:
                    {
                        return parentElementName + "_" + CiUniqueIdFunctionMapper[Class].Invoke((Engine)engine, properties, pk);
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

        public static string GetEvolutionChassisUniqueID(Engine engine, List<Property> properties, string pk)
        {
            var labelProperty = properties.FirstOrDefault(x => x.Name.Equals("u_label"));

            var serialNumberProperty = properties.FirstOrDefault(x => x.Name.Equals("u_serial_number"));

            return labelProperty != null && serialNumberProperty != null ? serialNumberProperty.Value + "_" + pk + "_" + labelProperty.Value : String.Empty;
        }

        public static string GetEvolutionProtocolProcessorUniqueID(Engine engine, List<Property> properties, string pk)
        {
            //TODO: Change the method to use the configured PP Name

            string uniqueID = "ppstack " + pk.Split('_').First();

            var labelProperty = properties.FirstOrDefault(x => x.Name.Equals("u_label"));

            if (labelProperty != null)
            {
                labelProperty.Value = uniqueID;
            }

            return uniqueID;
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

        public string Class { get; set; }

        public string Value { get; set; }

        public Property(string name, string value)
        {
            Name = name;
            Class = String.Empty;
            Value = GetPropertyValueByName(value);
        }

        public Property(string name, string className, string value)
        {
            Name = name;
            Class = className;
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

        public string Class { get; set; }

        public KeyValuePair<int, int> ParameterIdxByPid { get; set; }

        public string CurrentValue { get; set; }

        public string PreviousValue { get; set; }

        public ParameterDetails(string attributeName, string className, KeyValuePair<int, int> paramIdxByPid)
        {
            AttributeName = attributeName;
            Class = className;
            ParameterIdxByPid = paramIdxByPid;
            CurrentValue = String.Empty;
            PreviousValue = String.Empty;
        }

        public ParameterDetails(string attributeName, string className, string currentValue)
        {
            AttributeName = attributeName;
            Class = className;
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

        public PropertyLink PropertyLink { get; set; }

        public string Type { get; set; }

        public bool IsMappedFromParent { get; set; }

        public Relationship(string childClass, string parentClass, string linkProperty, string externalProperty, string externalClass, string type, bool isMappedFromParent)
        {
            Name = childClass + "/" + type + "/" + parentClass;
            ParentClass = parentClass;
            ChildClass = childClass;
            PropertyLink = new PropertyLink(linkProperty, externalProperty, externalClass);
            Type = type;
            IsMappedFromParent = isMappedFromParent;
        }
    }

    public class PropertyLink
    {
        public string Name { get; set; }

        public Property ExternalProperty { get; set; }

        public PropertyLink(string name, string externalProperty, string externalClass)
        {
            Name = name;
            ExternalProperty = new Property(externalProperty, externalClass, String.Empty);
        }
    }
}
