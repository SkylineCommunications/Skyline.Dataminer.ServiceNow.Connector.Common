namespace Skyline.DataMiner.ServiceNow.Connector.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using global::Skyline.DataMiner.Automation;
    using Newtonsoft.Json;
    using static Skyline.DataMiner.ServiceNow.Connector.Common.Connector;

    /// <summary>
    /// Data structure used to perform mappings between ServiceNow CIs and its properties and the respective Dataminer counterparts.
    /// </summary>
    public class Connector
    {
        /// <summary>
        /// Describes the different options that can be used to build a CI unique ID.
        /// </summary>
        public enum NamingFormat
        {
            // TODO: Check which prefix better serves the purpose of protecting ID uniqueness (Element DMA ID might cause trouble if swarming is used in the future)
            // Naming format always includes parent element name to avoid duplicating unique IDs (for instance in case there are duplicate elements)
            /// <summary>
            /// Unique ID format is undefined.
            /// </summary>
            Unknown,
            /// <summary>
            /// Unique ID is the instance primary key.
            /// </summary>
            PrimaryKey,
            /// <summary>
            /// Unique ID is formed by a combination between instance primary key and label.
            /// </summary>
            PrimaryKey_Label,
            /// <summary>
            /// Unique ID is instance label parameter.
            /// </summary>
            Label,
            /// <summary>
            /// Unique ID is formed by a combination between instance label and primary key.
            /// </summary>
            Label_PrimaryKey,
            /// <summary>
            /// Unique ID is retrieved from a custom method.
            /// </summary>
            Custom,
        }

        private static Dictionary<string, List<ConnectorMapping>> connectorMappingsBySystem;

        /// <summary>
        /// Data structure that details the connector mappings for the systems supported by the integrating solution.
        /// </summary>
        public static Dictionary<string, List<ConnectorMapping>> Mappings
        {
            get
            {
                if (connectorMappingsBySystem != null)
                {
                    return connectorMappingsBySystem;
                }

                connectorMappingsBySystem = new Dictionary<string, List<ConnectorMapping>>
                {
                    // TODO: Include all supported Connector/CI Types mappings here
                    {
                        "iDirect Evolution", new List<ConnectorMapping>
                        {
                            new ConnectorMapping(
                                "iDirect Platform",
                                new List<ClassMapping>
                                {
                                    new ClassMapping
                                    {
                                        Class = "Evolution NMS",
                                        TargetTable = "u_cmdb_ci_appl_nms_evolution",
                                        IsParent = true,
                                        NamingDetails = new NamingDetails(NamingFormat.PrimaryKey, new PropertyLink()),
                                        AttributesByTableID = new Dictionary<int, List<ClassProperty>>(),
                                    },
                                    new ClassMapping
                                    {
                                        Class = "Evolution Remote",
                                        TargetTable = "u_cmdb_ci_modem_evolution_remote",
                                        IsParent = false,
                                        NamingDetails = new NamingDetails(NamingFormat.Label, new PropertyLink()),
                                        AttributesByTableID = ClassPropertiesMapper["Evolution Remote"].Invoke(),
                                    },
                                    new ClassMapping
                                    {
                                        Class = "Evolution Linecard",
                                        TargetTable = "u_cmdb_ci_modem_evolution_linecard",
                                        IsParent = false,
                                        NamingDetails = new NamingDetails(NamingFormat.Label, new PropertyLink()),
                                        AttributesByTableID = ClassPropertiesMapper["Evolution Linecard"].Invoke(),
                                    },
                                    new ClassMapping
                                    {
                                        Class = "Evolution Network",
                                        TargetTable = "u_cmdb_ci_group_evolution_network",
                                        IsParent = false,
                                        NamingDetails = new NamingDetails(NamingFormat.Label, new PropertyLink()),
                                        AttributesByTableID = ClassPropertiesMapper["Evolution Network"].Invoke(),
                                    },
                                    new ClassMapping
                                    {
                                        Class = "Evolution Chassis",
                                        TargetTable = "cmdb_ci_chassis",
                                        IsParent = false,
                                        NamingDetails = new NamingDetails(NamingFormat.Label, new PropertyLink()),
                                        AttributesByTableID = ClassPropertiesMapper["Evolution Chassis"].Invoke(),
                                    },
                                    new ClassMapping
                                    {
                                        Class = "Evolution Inroute Group",
                                        TargetTable = "u_cmdb_ci_evolution_inroute_group",
                                        IsParent = false,
                                        NamingDetails = new NamingDetails(NamingFormat.Label, new PropertyLink()),
                                        AttributesByTableID = ClassPropertiesMapper["Evolution Inroute Group"].Invoke(),
                                    },
                                    new ClassMapping
                                    {
                                        Class = "Evolution Protocol Processor",
                                        TargetTable = "u_cmdb_ci_appl_evolution_pp",
                                        IsParent = false,
                                        NamingDetails = new NamingDetails(NamingFormat.Label, new PropertyLink()),
                                        //NamingDetails = new NamingDetails(NamingFormat.Custom, new List<string> { "u_label", "u_ppb_network_id" }, new ExternalPropertyLink("u_network_pp_name", "Evolution Network", "u_pp_id", "Evolution Network")),
                                        AttributesByTableID = ClassPropertiesMapper["Evolution Protocol Processor"].Invoke(),
                                    },
                                    new ClassMapping
                                    {
                                        Class = "Evolution Protocol Processor Blade",
                                        TargetTable = "u_cmdb_ci_appl_evolution_pp_blade",
                                        IsParent = false,
                                        NamingDetails = new NamingDetails(NamingFormat.Label, new PropertyLink()),
                                        AttributesByTableID = ClassPropertiesMapper["Evolution Protocol Processor Blade"].Invoke(),
                                    },
                                    new ClassMapping
                                    {
                                        Class = "Evolution Teleport",
                                        TargetTable = "u_cmdb_ci_group_evolution_teleport",
                                        IsParent = false,
                                        NamingDetails = new NamingDetails(NamingFormat.Label, new PropertyLink()),
                                        AttributesByTableID = ClassPropertiesMapper["Evolution Teleport"].Invoke(),
                                    },
                                    //new ClassMapping
                                    //{
                                    //    Class = "Evolution Application",
                                    //    TargetTable = "u_cmdb_ci_appl_evolution",
                                    //    IsParent = false,
                                    //    NamingDetails = new NamingDetails(NamingFormat.Label, new List<string>(), new ExternalPropertyLink()),
                                    //    AttributesByTableID = new Dictionary<int, List<ClassProperty>>(),
                                    //},
                                    //new ClassMapping
                                    //{
                                    //    Class = "Evolution Encapsulator",
                                    //    TargetTable = "u_cmdb_ci_appl_evolution_encapsulator",
                                    //    IsParent = false,
                                    //    NamingDetails = new NamingDetails(NamingFormat.Label, new List<string>(), new ExternalPropertyLink()),
                                    //    AttributesByTableID = new Dictionary<int, List<ClassProperty>>(),
                                    //},
                                    //new ClassMapping
                                    //{
                                    //    Class = "Evolution Processing Node",
                                    //    TargetTable = "u_cmdb_ci_appl_evolution_processing_node",
                                    //    IsParent = false,
                                    //    NamingDetails = new NamingDetails(NamingFormat.Label, new List<string>(), new ExternalPropertyLink()),
                                    //    AttributesByTableID = new Dictionary<int, List<ClassProperty>>(),
                                    //},
                                },
                                new List<Relationship>
                                {
                                    // TODO: Add class relationships here
                                    new Relationship("Evolution NMS", "Evolution Remote", new List<PropertyLink> { new PropertyLink(String.Empty, "u_nms_name") }, new List<PropertyLink> { }, "Managed by::Manages", true),
                                    new Relationship("Evolution NMS", "Evolution Linecard", new List<PropertyLink> { new PropertyLink(String.Empty, "u_nms_name") }, new List<PropertyLink> { }, "Managed by::Manages", true),
                                    new Relationship("Evolution NMS", "Evolution Network", new List<PropertyLink> { new PropertyLink(String.Empty, "u_nms_name") }, new List<PropertyLink> { }, "Managed by::Manages", true),
                                    new Relationship("Evolution NMS", "Evolution Chassis", new List<PropertyLink> { new PropertyLink(String.Empty, "u_nms_name") }, new List<PropertyLink> { }, "Managed by::Manages", true),
                                    new Relationship("Evolution NMS", "Evolution Inroute Group", new List<PropertyLink> { new PropertyLink(String.Empty, "u_nms_name") }, new List<PropertyLink> { }, "Managed by::Manages", true),
                                    new Relationship("Evolution NMS", "Evolution Protocol Processor", new List<PropertyLink> { new PropertyLink(String.Empty, "u_nms_name") }, new List<PropertyLink> { }, "Managed by::Manages", true),
                                    new Relationship("Evolution NMS", "Evolution Protocol Processor Blade", new List<PropertyLink> { new PropertyLink(String.Empty, "u_nms_name") }, new List<PropertyLink> { }, "Managed by::Manages", true),
                                    new Relationship("Evolution NMS", "Evolution Teleport", new List<PropertyLink> { new PropertyLink(String.Empty, "u_nms_name") }, new List<PropertyLink> { }, "Managed by::Manages", true),
                                    new Relationship("Evolution Inroute Group", "Evolution Remote", new List<PropertyLink> { new PropertyLink(String.Empty, "u_inroute_group") }, new List<PropertyLink> { }, "Connected by::Connects", true),
                                    new Relationship("Evolution Linecard", "Evolution Inroute Group", new List<PropertyLink> { new PropertyLink("u_inroute_group", String.Empty) }, new List<PropertyLink> { }, "Connected by::Connects", false),
                                    new Relationship("Evolution Remote", "Evolution Linecard", new List<PropertyLink> { new PropertyLink("u_network_id", "u_network_id"), new PropertyLink(String.Empty, "u_type") }, new List<PropertyLink> { }, "Receives data from::Sends data to", false),
                                    new Relationship("Evolution Network", "Evolution Linecard", new List<PropertyLink> { new PropertyLink("u_network_id", "u_chassis_slot_id") }, new List<PropertyLink> { }, "Depends on::Used by", true),
                                    new Relationship("Evolution Chassis", "Evolution Linecard", new List<PropertyLink> { new PropertyLink("u_chassis_id", "u_chassis_slot_id") }, new List<PropertyLink> { }, "Located in::Houses", true),
                                    new Relationship("Evolution Protocol Processor", "Evolution Network", new List<PropertyLink> { new PropertyLink(String.Empty, "u_network_pp_name") }, new List<PropertyLink> { }, "Depends on::Used by", true),
                                    new Relationship("Evolution Protocol Processor Blade", "Evolution Protocol Processor", new List<PropertyLink> { new PropertyLink("u_ppb_network_id", "u_network_id") }, new List<PropertyLink> { }, "Depends on::Used by", false),
                                    new Relationship("Evolution Linecard", "Evolution Linecard", new List<PropertyLink> { new PropertyLink(String.Empty, "u_redundancy_linecard") }, new List<PropertyLink> { }, "DR provided by::Provides DR for", true),
                                    new Relationship("Evolution Protocol Processor", "Evolution Teleport", new List<PropertyLink> { new PropertyLink(String.Empty, String.Empty) }, new List<PropertyLink> { }, "Contains::Contained By", true),
                                    new Relationship("Evolution Chassis", "Evolution Teleport", new List<PropertyLink> { new PropertyLink(String.Empty, String.Empty) }, new List<PropertyLink> { }, "Contains::Contained By", true),
                                })
                            }
                    },
                    {
                        "Newtec Dialog", new List<ConnectorMapping>
                        {
                            new ConnectorMapping(
                                "Newtec Dialog Time Series Database",
                                new List<ClassMapping>
                                {
                                    new ClassMapping
                                    {
                                        Class = "Dialog NMS",
                                        TargetTable = "u_cmdb_ci_appl_nms_dialog",
                                        IsParent = true,
                                        NamingDetails = new NamingDetails(NamingFormat.PrimaryKey, new PropertyLink()),
                                        AttributesByTableID = new Dictionary<int, List<ClassProperty>>(),
                                    },
                                    new ClassMapping
                                    {
                                        Class = "Dialog Remote",
                                        TargetTable = "u_cmdb_ci_modem_dialog_remote",
                                        IsParent = false,
                                        NamingDetails = new NamingDetails(NamingFormat.Label, new PropertyLink()),
                                        AttributesByTableID = ClassPropertiesMapper["Dialog Remote"].Invoke(),
                                    },
                                    new ClassMapping
                                    {
                                        Class = "Dialog Satellite Network",
                                        TargetTable = "u_cmdb_ci_dialog_satellite_network",
                                        IsParent = false,
                                        NamingDetails = new NamingDetails(NamingFormat.Custom, new PropertyLink()),
                                        AttributesByTableID = ClassPropertiesMapper["Dialog Satellite Network"].Invoke(),
                                    },
                                },
                                new List<Relationship>
                                {
                                    // TODO: Add class relationships here
                                    new Relationship("Dialog NMS", "Dialog Remote", new List<PropertyLink> { new PropertyLink(String.Empty, "u_nms_name") }, new List<PropertyLink> { }, "Managed by::Manages", true),
                                    new Relationship("Dialog NMS", "Dialog Satellite Network", new List<PropertyLink> { new PropertyLink(String.Empty, "u_nms_name") }, new List<PropertyLink> { }, "Managed by::Manages", true),
                                    new Relationship("Dialog Remote", "Dialog Satellite Network", new List<PropertyLink> { new PropertyLink("u_satnet", "u_label") }, new List<PropertyLink> { }, "Receives data from::Sends data to", false),
                                }),
                            new ConnectorMapping(
                                "Newtec Dialog Infrastructure",
                                new List<ClassMapping>
                                {
                                    new ClassMapping
                                    {
                                        Class = "Dialog Hub",
                                        TargetTable = "u_cmdb_ci_dialog_hub",
                                        IsParent = true,
                                        NamingDetails = new NamingDetails(NamingFormat.Label, new PropertyLink()),
                                        AttributesByTableID = new Dictionary<int, List<ClassProperty>>(),
                                    },
                                    new ClassMapping
                                    {
                                        Class = "Dialog Modulator",
                                        TargetTable = "u_cmdb_ci_dialog_modulator",
                                        IsParent = false,
                                        NamingDetails = new NamingDetails(NamingFormat.Custom, new PropertyLink()),
                                        AttributesByTableID = ClassPropertiesMapper["Dialog Modulator"].Invoke(),
                                    },
                                    new ClassMapping
                                    {
                                        Class = "Dialog Demodulator",
                                        TargetTable = "u_cmdb_ci_dialog_demodulator",
                                        IsParent = false,
                                        NamingDetails = new NamingDetails(NamingFormat.Custom, new PropertyLink()),
                                        AttributesByTableID = ClassPropertiesMapper["Dialog Demodulator"].Invoke(),
                                    },
                                    new ClassMapping
                                    {
                                        Class = "Dialog Switch",
                                        TargetTable = "cmdb_ci_ip_switch",
                                        IsParent = false,
                                        NamingDetails = new NamingDetails(NamingFormat.Custom, new PropertyLink()),
                                        AttributesByTableID = ClassPropertiesMapper["Dialog Switch"].Invoke(),
                                    },
                                    new ClassMapping
                                    {
                                        Class = "Dialog Enclosure",
                                        TargetTable = "cmdb_ci_enclosure",
                                        IsParent = false,
                                        NamingDetails = new NamingDetails(NamingFormat.Label, new PropertyLink()),
                                        AttributesByTableID = ClassPropertiesMapper["Dialog Enclosure"].Invoke(),
                                    },
                                    new ClassMapping
                                    {
                                        Class = "Dialog Linux Server",
                                        TargetTable = "cmdb_ci_linux_server",
                                        IsParent = false,
                                        NamingDetails = new NamingDetails(NamingFormat.Custom, new PropertyLink("u_hub_nms", "u_nms_name", "Dialog NMS", "Dialog NMS")),
                                        AttributesByTableID = ClassPropertiesMapper["Dialog Linux Server"].Invoke(),
                                    },
                                    new ClassMapping
                                    {
                                        Class = "Dialog MS Server",
                                        TargetTable = "cmdb_ci_win_server",
                                        IsParent = false,
                                        NamingDetails = new NamingDetails(NamingFormat.Label, new PropertyLink()),
                                        AttributesByTableID = ClassPropertiesMapper["Dialog MS Server"].Invoke(),
                                    },
                                    new ClassMapping
                                    {
                                        Class = "Dialog Application",
                                        TargetTable = "u_cmdb_ci_dialog_application",
                                        IsParent = false,
                                        NamingDetails = new NamingDetails(NamingFormat.Label, new PropertyLink()),
                                        AttributesByTableID = new Dictionary<int, List<ClassProperty>>(),
                                    },
                                },
                                new List<Relationship>
                                {
                                    // TODO: Add class relationships here
                                    new Relationship("Dialog Hub", "Dialog Modulator", new List<PropertyLink> { new PropertyLink(String.Empty, "u_nms_name") }, new List<PropertyLink> { }, "Managed by::Manages", true),
                                    new Relationship("Dialog Hub", "Dialog Demodulator", new List<PropertyLink> { new PropertyLink(String.Empty, "u_nms_name") }, new List<PropertyLink> { }, "Managed by::Manages", true),
                                    new Relationship("Dialog Hub", "Dialog Switch", new List<PropertyLink> { new PropertyLink(String.Empty, "u_nms_name") }, new List<PropertyLink> { }, "Managed by::Manages", true),
                                    new Relationship("Dialog Hub", "Dialog Enclosure", new List<PropertyLink> { new PropertyLink(String.Empty, "u_nms_name") }, new List<PropertyLink> { }, "Managed by::Manages", true),
                                    new Relationship("Dialog Hub", "Dialog Linux Server", new List<PropertyLink> { new PropertyLink(String.Empty, "u_nms_name") }, new List<PropertyLink> { }, "Depends on::Used by", true),
                                    new Relationship("Dialog Hub", "Dialog MS Server", new List<PropertyLink> { new PropertyLink(String.Empty, "u_nms_name") }, new List<PropertyLink> { }, "Depends on::Used by", true),
                                    //new Relationship("Dialog Satellite Network", "Dialog Hub", new List<PropertyLink> { new PropertyLink(String.Empty, "u_nms_name") }, new List<PropertyLink> { }, "Depends on::Used by", true),
                                    new Relationship("Dialog Demodulator", "Dialog Demodulator", new List<PropertyLink> { new PropertyLink("u_hps_id", "u_hps_id"), new PropertyLink("u_dp_id", "u_dp_id"), new PropertyLink("u_role_id", "u_role_id") }, new List<PropertyLink> { }, "DR provided by::Provides DR for", true),
                                   // new Relationship("Dialog Demodulator", "Dialog Switch", new List<PropertyLink> { }, new List<PropertyLink> { }, "Uses::Used by", true),
                                    new Relationship("Dialog Demodulator", "Dialog Satellite Network", new List<PropertyLink> { new PropertyLink("u_hps_chain", "u_hps_name") }, new List<PropertyLink> { }, "Depends on::Used by", true),
                                    new Relationship("Dialog Modulator", "Dialog Modulator", new List<PropertyLink> { new PropertyLink("u_hps_id", "u_hps_id"), new PropertyLink("u_dp_id", "u_dp_id"), new PropertyLink("u_role_id", "u_role_id") }, new List<PropertyLink> { }, "DR provided by::Provides DR for", true),
                                    new Relationship("Dialog Modulator", "Dialog Switch", new List<PropertyLink> { new PropertyLink("u_hps_chain", "u_hps_chain") }, new List<PropertyLink> { }, "Uses::Used by", true),
                                    new Relationship("Dialog Modulator", "Dialog Satellite Network", new List<PropertyLink> { new PropertyLink("u_hps_chain", "u_hps_name") }, new List<PropertyLink> { }, "Depends on::Used by", true),
                                    new Relationship("Dialog Enclosure", "Dialog Linux Server", new List<PropertyLink> { new PropertyLink(String.Empty, "u_hub_name") }, new List<PropertyLink> { }, "Located in::Houses", true),
                                    //new Relationship("Dialog MS Server", "Dialog Linux Server", new List<PropertyLink> { new PropertyLink("u_hub_name", "u_hub_name") }, new List<PropertyLink> { }, "Virtualized::Virtualizes", true),
                                }),
                        }
                    },
                };

                return connectorMappingsBySystem;
            }
        }

        private static Dictionary<string, Func<Dictionary<int, List<ClassProperty>>>> classPropertiesMapper;

        /// <summary>
        /// Data structure that provides the methods used to retrieve the property mappings for each of the supported CI Classes
        /// </summary>
        public static Dictionary<string, Func<Dictionary<int, List<ClassProperty>>>> ClassPropertiesMapper
        {
            get
            {
                if (classPropertiesMapper != null)
                {
                    return classPropertiesMapper;
                }

                classPropertiesMapper = new Dictionary<string, Func<Dictionary<int, List<ClassProperty>>>>
                {
                    // TODO: Add class property mapping methods here
                    // iDirect Platform
                    { "Evolution Remote", GetEvolutionRemoteClassProperties },
                    { "Evolution Linecard", GetEvolutionLinecardClassProperties },
                    { "Evolution Network", GetEvolutionNetworkClassProperties },
                    { "Evolution Chassis", GetEvolutionChassisClassProperties },
                    { "Evolution Inroute Group", GetEvolutionInrouteGroupClassProperties },
                    { "Evolution Protocol Processor", GetEvolutionProtocolProcessorClassProperties },
                    { "Evolution Protocol Processor Blade", GetEvolutionProtocolProcessorBladeClassProperties },
                    { "Evolution Teleport", GetEvolutionTeleportClassProperties },
                    // Dialog TSDB
                    { "Dialog Remote", GetDialogRemoteClassProperties },
                    { "Dialog Satellite Network", GetDialogSatelliteNetworkClassProperties },
                    // Dialog Infrastructure
                    { "Dialog Modulator", GetDialogModulatorClassProperties },
                    { "Dialog Demodulator", GetDialogDemodulatorClassProperties },
                    { "Dialog Switch", GetDialogSwitchClassProperties },
                    { "Dialog Enclosure", GetDialogEnclosureClassProperties },
                    { "Dialog MS Server", GetDialogMicrosoftServerClassProperties },
                    { "Dialog Linux Server", GetDialogLinuxServerClassProperties },
                };

                return classPropertiesMapper;
            }
        }

        private static Dictionary<int, List<ClassProperty>> GetEvolutionRemoteClassProperties()
        {
            return new Dictionary<int, List<ClassProperty>>
            {
                //  TODO: Add attributes here
                {
                    300,
                    new List<ClassProperty>
                    {
                        new ClassProperty("pk", 0, false, false, false),
                        new ClassProperty("u_label", 1, false, false, true),
                        new ClassProperty("u_status", 6, true, false, false),
                        new ClassProperty("u_network_id", 9, true, false, false),
                        new ClassProperty("u_network_name", 10, false, false, false),
                        new ClassProperty("u_inroute_group_id", 11, false, false, false),
                        new ClassProperty("u_inroute_group", 12, false, false, false),
                        new ClassProperty("u_customer_id", 13, false, false, false),
                        new ClassProperty("u_active_sw_version", 14, false, false, false),
                        new ClassProperty("u_hw_type", 15, false, false, false),
                        new ClassProperty("u_pp_id", 16, false, false, false),
                        new ClassProperty("serial_number", 17, false, true, false),
                        new ClassProperty("u_nms_name", 18, false, false, false),
                    }
                },
            };
        }

        private static Dictionary<int, List<ClassProperty>> GetEvolutionLinecardClassProperties()
        {
            return new Dictionary<int, List<ClassProperty>>
            {
                //  TODO: Add attributes here
                {
                    400,
                    new List<ClassProperty>
                    {
                        new ClassProperty("pk", 0, false, false, false),
                        new ClassProperty("u_label", 1, false, false, true),
                        new ClassProperty("u_type", 3, false, false, true),
                        new ClassProperty("u_status", 4, true, false, false),
                        new ClassProperty("u_inroute_group", 8, false, false, false),
                        new ClassProperty("u_customer_id", 9, false, false, false),
                        new ClassProperty("u_active_sw_version", 10, false , false, false),
                        new ClassProperty("u_hw_type", 11, false, false, false),
                        new ClassProperty("u_pp_id", 12, false, false, false),
                        new ClassProperty("serial_number", 13, false, true, false),
                        new ClassProperty("u_chassis_id", 14, false, false, false),
                        new ClassProperty("u_chassis_slot_number", 15, false, false, false),
                        new ClassProperty("u_network_id", 16, true, false, false),
                        new ClassProperty("u_nms_name", 17, false, false, false),
                    }
                },
                {
                    1700,
                    new List<ClassProperty>
                    {
                        new ClassProperty("u_chassis_slot_id", 1, true, false, false),
                        new ClassProperty("fk", 6, false, false, false),
                        new ClassProperty("u_redundancy_linecard", 8, true, false, false),
                    }
                },
            };
        }

        private static Dictionary<int, List<ClassProperty>> GetEvolutionNetworkClassProperties()
        {
            return new Dictionary<int, List<ClassProperty>>
            {
                //  TODO: Add attributes here
                {
                    600,
                    new List<ClassProperty>
                    {
                        new ClassProperty("pk", 0, false, false, false),
                        new ClassProperty("u_network_id", 0, false, false, false),
                        new ClassProperty("u_label", 1, false, false, true),
                        new ClassProperty("u_status", 3, true, false, false),
                        new ClassProperty("u_teleport_id", 6, false, false, false),
                        new ClassProperty("u_pp_id", 7, false, false, false),
                        new ClassProperty("u_nms_name", 8, false, false, false),
                    }
                },
                {
                    6000,
                    new List<ClassProperty>
                    {
                        new ClassProperty("pk", 0, false, false, false),
                        new ClassProperty("u_network_pp_name", 30, false, false, false),
                    }
                },
            };
        }

        private static Dictionary<int, List<ClassProperty>> GetEvolutionChassisClassProperties()
        {
            return new Dictionary<int, List<ClassProperty>>
            {
                //  TODO: Add attributes here
                {
                    1500,
                    new List<ClassProperty>
                    {
                        new ClassProperty("pk", 0, false, false, false),
                        new ClassProperty("u_chassis_id", 0, false, false, false),
                        new ClassProperty("u_label", 1, false, false, true),
                        new ClassProperty("serial_number", 2, false, true, false),
                        new ClassProperty("u_status", 3, true, false, false),
                        new ClassProperty("u_nms_ip", 4, false, false, false),
                        new ClassProperty("u_nms_name", 5, false, false, false),
                    }
                },
            };
        }

        private static Dictionary<int, List<ClassProperty>> GetEvolutionInrouteGroupClassProperties()
        {
            return new Dictionary<int, List<ClassProperty>>
            {
                //  TODO: Add attributes here
                {
                    7400,
                    new List<ClassProperty>
                    {
                        new ClassProperty("pk", 0, false, false, false),
                        new ClassProperty("u_label", 1, false, false, true),
                        new ClassProperty("u_network_id", 2, true, false, false),
                        new ClassProperty("u_nms_name", -1, false, false, false),
                    }
                },
            };
        }

        private static Dictionary<int, List<ClassProperty>> GetEvolutionProtocolProcessorClassProperties()
        {
            return new Dictionary<int, List<ClassProperty>>
            {
                //  TODO: Add attributes here
                {
                    6000,
                    new List<ClassProperty>
                    {
                        // TODO: PP: Validate for multiple networks
                        new ClassProperty("pk", 0, false, false, false),
                        new ClassProperty("u_network_id", 0, false, false, false),
                        new ClassProperty("u_label", 30, true, false, false),
                        new ClassProperty("u_nms_name", -1, false, false, false),
                    }
                },
            };
        }

        private static Dictionary<int, List<ClassProperty>> GetEvolutionProtocolProcessorBladeClassProperties()
        {
            return new Dictionary<int, List<ClassProperty>>
            {
                //  TODO: Add attributes here
                {
                    6300,
                    new List<ClassProperty>
                    {
                        new ClassProperty("pk", 0, false, false, false),
                        new ClassProperty("u_label", 1, false, false, true),
                        new ClassProperty("u_ppb_network_id", 2, true, false, false),
                        new ClassProperty("u_nms_name", -1, false, false, false),
                    }
                },
            };
        }

        private static Dictionary<int, List<ClassProperty>> GetEvolutionTeleportClassProperties()
        {
            return new Dictionary<int, List<ClassProperty>>
            {
                //  TODO: Add attributes here
                {
                    6000,
                    new List<ClassProperty>
                    {
                        // TODO: Teleport: Validate for multiple networks
                        new ClassProperty("pk", 0, false, false, false),
                        new ClassProperty("u_network_id", 0, false, false, false),
                        new ClassProperty("u_label", 18, true, false, true),
                        new ClassProperty("u_nms_name", -1, false, false, false),
                    }
                },
            };
        }

        private static Dictionary<int, List<ClassProperty>> GetDialogRemoteClassProperties()
        {
            return new Dictionary<int, List<ClassProperty>>
            {
                //  TODO: Add attributes here
                //{
                //    12500,
                //    new List<ClassProperty>
                //    {
                //        new ClassProperty("pk", 0, false, false, false),
                //        new ClassProperty("u_label", 11, false, false, false),
                //        new ClassProperty("u_modem_name", 1, false, false, false),
                //        new ClassProperty("u_modem_type", 2, false, false, false),
                //        new ClassProperty("u_return_technology", 3, false, false, false),
                //        new ClassProperty("u_mac_address", 4, false, false, false),
                //        new ClassProperty("u_monitoring_type", 5, false, false, false),
                //        new ClassProperty("u_nms_name", -1, false, false, false),
                //    }
                //},
                //{
                //    12700,
                //    new List<ClassProperty>
                //    {
                //        new ClassProperty("pk", 0, false, false, false),
                //        new ClassProperty("u_network_name", 6, false, false, false),
                //        new ClassProperty("u_network_config", 8, false, false, false),
                //        new ClassProperty("u_sw_version", 9, false, false, false),
                //        new ClassProperty("u_last_network_config", 10, false, false),
                //        new ClassProperty("u_status", 149, true, false, false),
                //        new ClassProperty("u_nms_name", -1, false, false, false),
                //    }
                //},
                //{
                //    21000,
                //    new List<ClassProperty>
                //    {
                //        new ClassProperty("u_beam_state", 2, false, false, false),
                //        new ClassProperty("u_active_beam", 5, true, false, false),
                //        new ClassProperty("fk", 7, false, false, false),
                //    }
                //},
                {
                    100,
                    new List<ClassProperty>
                    {
                        new ClassProperty("pk", 0, false, false, false),
                        new ClassProperty("u_label", 1, false, false, true),
                        new ClassProperty("u_satnet", 44, true, false, false),
                        new ClassProperty("u_nms_name", -1, false, false, false),
                    }
                },
            };
        }

        private static Dictionary<int, List<ClassProperty>> GetDialogSatelliteNetworkClassProperties()
        {
            return new Dictionary<int, List<ClassProperty>>
            {
                //  TODO: Add attributes here
                //{
                //    15000,
                //    new List<ClassProperty>
                //    {
                //        new ClassProperty("pk", 0, false, false, false),
                //        new ClassProperty("u_label", 18, false, false, false),
                //        new ClassProperty("u_nms_name", -1, false, false, false),
                //    }
                //},
                {
                    4300,
                    new List<ClassProperty>
                    {
                        new ClassProperty("pk", 0, false, false, false),
                        new ClassProperty("u_beam_name", 4, false, false, false),
                        new ClassProperty("u_hps_name", 22, true, false, true),
                        new ClassProperty("u_label", 26, false, false, true),
                        new ClassProperty("u_nms_name", -1, false, false, false),
                    }
                },
            };
        }

        private static Dictionary<int, List<ClassProperty>> GetDialogModulatorClassProperties()
        {
            return new Dictionary<int, List<ClassProperty>>
            {
                //  TODO: Add attributes here
                {
                    3100,
                    new List<ClassProperty>
                    {
                        new ClassProperty("pk", 0, false, false, false),
                        new ClassProperty("u_label", -1, false, false, false),
                        new ClassProperty("u_label_device", 4, false, false, true),
                        new ClassProperty("u_hps_id", 6, false, false, false),
                        new ClassProperty("u_role_id", 7, true, false, false),
                        new ClassProperty("u_dp_id", 9, false, false, false),
                        new ClassProperty("u_nms_name", -1, false, false, false),
                    }
                },
                {
                    2300,
                    new List<ClassProperty>
                    {
                        new ClassProperty("pk", 0, false, false, false),
                        new ClassProperty("u_label", -1, false, false, false),
                        new ClassProperty("u_label_mcm7500", 2, false, false, true),
                        new ClassProperty("u_hps_id", -1, false, false, false),
                        new ClassProperty("u_role_id", -1, false, false, false),
                        new ClassProperty("u_dp_id", -1, false, false, false),
                        new ClassProperty("u_nms_name", -1, false, false, false),
                    }
                },
                {
                    4300,
                    new List<ClassProperty>
                    {
                        new ClassProperty("pk", 0, false, false, false),
                        new ClassProperty("u_label", -1, false, false, false),
                        new ClassProperty("u_label_m6100", 2, false, false, true),
                        new ClassProperty("u_hps_id", -1, false, false, false),
                        new ClassProperty("u_role_id", -1, false, false, false),
                        new ClassProperty("u_dp_id", -1, false, false, false),
                        new ClassProperty("u_nms_name", -1, false, false, false),
                    }
                },
                {
                    3200,
                    new List<ClassProperty>
                    {
                        new ClassProperty("u_label_1_1", 4, false, false, false),
                        new ClassProperty("u_chain_id", 8, false, false, false),
                    }
                },
                {
                    3000,
                    new List<ClassProperty>
                    {
                        new ClassProperty("u_hps_chain", 4, false, false, false),
                        new ClassProperty("u_active_chain", 5, false, false, false),
                    }
                },
            };
        }

        private static Dictionary<int, List<ClassProperty>> GetDialogDemodulatorClassProperties()
        {
            return new Dictionary<int, List<ClassProperty>>
            {
                //  TODO: Add attributes here
                {
                    3100,
                    new List<ClassProperty>
                    {
                        new ClassProperty("pk", 0, false, false, false),
                        new ClassProperty("u_label", 4, false, false, true),
                        new ClassProperty("u_hps_id", 6, false, false, false),
                        new ClassProperty("u_role_id", 7, true, false, false),
                        new ClassProperty("u_dp_id", 9, false, false, false),
                        new ClassProperty("u_nms_name", -1, false, false, false),
                    }
                },
            };
        }

        private static Dictionary<int, List<ClassProperty>> GetDialogSwitchClassProperties()
        {
            return new Dictionary<int, List<ClassProperty>>
            {
                //  TODO: Add attributes here
                {
                    5400,
                    new List<ClassProperty>
                    {
                        new ClassProperty("pk", 0, false, false, false),
                        new ClassProperty("u_label", -1, false, false, false),
                        new ClassProperty("u_label_access", 2, false, false, true),
                        new ClassProperty("u_nms_name", -1, false, false, false),
                    }
                },
                {
                    6450,
                    new List<ClassProperty>
                    {
                        new ClassProperty("pk", 0, false, false, false),
                        new ClassProperty("u_label", -1, false, false, false),
                        new ClassProperty("u_label_rf", 2, false, false, true),
                        new ClassProperty("u_nms_name", -1, false, false, false),
                    }
                },
                {
                    3200,
                    new List<ClassProperty>
                    {
                        new ClassProperty("u_label_1_1", 4, false, false, false),
                        new ClassProperty("u_chain_id", 8, false, false, false),
                    }
                },
                {
                    3000,
                    new List<ClassProperty>
                    {
                        new ClassProperty("u_hps_chain", 4, false, false, false),
                        new ClassProperty("u_active_chain", 5, false, false, false),
                    }
                },
            };
        }

        private static Dictionary<int, List<ClassProperty>> GetDialogEnclosureClassProperties()
        {
            return new Dictionary<int, List<ClassProperty>>
            {
                //  TODO: Add attributes here
                {
                    3800,
                    new List<ClassProperty>
                    {
                        new ClassProperty("pk", 0, false, false, false),
                        new ClassProperty("u_label", 1, false, false, true),
                        new ClassProperty("u_nms_name", -1, false, false, false),
                    }
                },
            };
        }

        private static Dictionary<int, List<ClassProperty>> GetDialogMicrosoftServerClassProperties()
        {
            return new Dictionary<int, List<ClassProperty>>
            {
                //  TODO: Add attributes here
                {
                    4000,
                    new List<ClassProperty>
                    {
                        new ClassProperty("pk", 0, false, false, false),
                        new ClassProperty("u_label", 2, false, false, true),
                        new ClassProperty("u_nms_name", -1, false, false, false),
                    }
                },
            };
        }

        private static Dictionary<int, List<ClassProperty>> GetDialogLinuxServerClassProperties()
        {
            return new Dictionary<int, List<ClassProperty>>
            {
                //  TODO: Add attributes here
                {
                    13000,
                    new List<ClassProperty>
                    {
                        new ClassProperty("pk", 0, false, false, false),
                        new ClassProperty("u_label", 4, false, false, true),
                        new ClassProperty("u_nms_name", -1, false, false, false),
                        //new ClassProperty("u_hub_nms", -1, false, false, false),
                    }
                },
                {
                    14400,
                    new List<ClassProperty>
                    {
                        new ClassProperty("pk", 0, false, false, false),
                        new ClassProperty("u_function_id", 2, false, false, false),
                        new ClassProperty("u_label", 4, false, false, true),
                        new ClassProperty("u_parent_blade_server", 5, false, false, false),
                        new ClassProperty("u_nms_name", -1, false, false, false),
                        new ClassProperty("u_hub_nms", -1, false, false, false),
                    }
                },
                {
                    14200,
                    new List<ClassProperty>
                    {
                        new ClassProperty("u_blade_server", 1, false, false, false),
                        new ClassProperty("u_hub_name", 7, false, false, true),
                    }
                },
                //{
                //    2900,
                //    new List<ClassProperty>
                //    {
                //        //new ClassProperty("u_server_pk", 0, false, false, false),
                //        new ClassProperty("u_server_fk", 2, false, false, false),
                //        new ClassProperty("u_server_name", 4, false, false, false),
                //        new ClassProperty("u_virtual_machines", 7, false, false, false),
                //    }
                //},
                //{
                //    3780,
                //    new List<ClassProperty>
                //    {
                //        //new ClassProperty("u_server_pk", 0, false, false, false),
                //        new ClassProperty("u_server_fk", 2, false, false, false),
                //        new ClassProperty("u_server_name", 4, false, false, false),
                //        new ClassProperty("u_served_satnet", 7, false, false, false),
                //    }
                //},
                //{
                //    2800,
                //    new List<ClassProperty>
                //    {
                //        new ClassProperty("u_hub_pk", 0, false, false, false),
                //        new ClassProperty("u_hub_name", 2, false, false, false),
                //    }
                //},
            };
        }

        private static Dictionary<string, Action<Engine, List<Property>>> relationshipPropertyProcessingMapper;

        /// <summary>
        /// Data structure that provides the methods used to process necessary property values for given supported CI Classes
        /// </summary>
        public static Dictionary<string, Action<Engine, List<Property>>> RelationshipPropertyProcessingMapper
        {
            get
            {
                if (relationshipPropertyProcessingMapper != null)
                {
                    return relationshipPropertyProcessingMapper;
                }

                relationshipPropertyProcessingMapper = new Dictionary<string, Action<Engine, List<Property>>>
                {
                    // TODO: Add class property processing methods here
                    // Dialog Infrastructure
                    { "Dialog Modulator", ProcessDialogModulatorRelationshipProperties },
                };

                return relationshipPropertyProcessingMapper;
            }
        }

        private static void ProcessDialogModulatorRelationshipProperties(Engine engine, List<Property> properties)
        {
            //engine.GenerateInformation("ProcessDialogModulatorRelationshipProperties| ************** Properties:\n\n" + JsonConvert.SerializeObject(properties) + "\n\n");

            var deviceLabelProperty = properties.FirstOrDefault(x => x.Name.Equals("u_label_device"));

            if (deviceLabelProperty != null && !String.IsNullOrWhiteSpace(deviceLabelProperty.Value))
            {
                var labelParts = deviceLabelProperty.Value.Split('.');

                if (labelParts.Length == 3) return;
            }

            string modulatorLabel = GetModulatorLabel(properties);

            if (String.IsNullOrWhiteSpace(modulatorLabel)) return;

            var modulatorLabelParts = modulatorLabel.Split('[');

            if (modulatorLabelParts.Length < 2) return;

            var roleIdProperty = properties.FirstOrDefault(x => x.Name.Equals("u_role_id"));

            modulatorLabel = modulatorLabelParts[0].Trim();

            if (roleIdProperty != null && !String.IsNullOrWhiteSpace(modulatorLabelParts[0]))
            {
                var redundancyStatus = modulatorLabelParts[1].Replace("]", String.Empty);

                roleIdProperty.Value = redundancyStatus.Equals("Active") ? "1" : "2";
            }

            modulatorLabelParts = modulatorLabel.Split('.');

            if (modulatorLabelParts.Length < 4) return;

            var hpsIdProperty = properties.FirstOrDefault(x => x.Name.Equals("u_hps_id"));

            if (hpsIdProperty != null)
            {
                hpsIdProperty.Value = modulatorLabelParts[1].Replace("HPS-", String.Empty);
            }

            var poolIdProperty = properties.FirstOrDefault(x => x.Name.Equals("u_pool_id"));

            if (poolIdProperty != null)
            {
                poolIdProperty.Value = modulatorLabelParts[2].Replace("DP-", String.Empty);
            }
        }

        private static string GetModulatorLabel(List<Property> properties)
        {
            var mcm7500LabelProperty = properties.FirstOrDefault(x => x.Name.Equals("u_label_mcm7500"));
            var mcm6100LabelProperty = properties.FirstOrDefault(x => x.Name.Equals("u_label_m6100"));

            if (mcm7500LabelProperty != null && !String.IsNullOrWhiteSpace(mcm7500LabelProperty.Value))
            {
                return mcm7500LabelProperty.Value;
            }

            if (mcm6100LabelProperty != null && !String.IsNullOrWhiteSpace(mcm6100LabelProperty.Value))
            {
                return mcm6100LabelProperty.Value;
            }

            return String.Empty;
        }

        /// <summary>
        /// Get elements running supported connectors as defined by the in the Connector class mappings.
        /// </summary>
        /// <param name="engine"></param>
        /// <returns>list of elements running supported connectors.</returns>
        public static List<Element> GetSupportedDmsElements(IEngine engine)
        {
            var elements = new List<Element>();

            var supportedConnectors = Mappings.SelectMany(x => x.Value).ToList();

            foreach (var connector in supportedConnectors)
            {
                try
                {
                    var foundElements = engine.FindElementsByProtocol(connector.ProtocolName);

                    if (foundElements.Length > 0)
                    {
                        elements.AddRange(foundElements);
                    }
                }
                catch (Exception)
                {
                    // Do nothing...
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
            var conenctorMappings = Mappings.SelectMany(x => x.Value).ToList();

            var connectorClassMappings = conenctorMappings.FirstOrDefault(m => m.ProtocolName.Equals(protocolName));

            if (connectorClassMappings != null)
            {
                var parameterUpdates = new List<ParameterDetails>();

                foreach (var classMapping in connectorClassMappings.ClassMappings)
                {
                    var classAttributesByTablePID = classMapping.AttributesByTableID;

                    foreach (var attributeKvp in classAttributesByTablePID)
                    {
                        var pushAttributes = attributeKvp.Value.Where(attribute => attribute.HasPushEvent).ToList();

                        if (pushAttributes.Count == 0) continue;

                        var namingAttributes = attributeKvp.Value.Where(attribute => attribute.IsNamingProperty).ToList();

                        pushAttributes.AddRange(namingAttributes);

                        var parameterDetails = pushAttributes
                            .Select(attribute => new ParameterDetails(attribute.Name, classMapping.Class, new KeyValuePair<int, int>(attributeKvp.Key, attribute.ColumnIdx), attribute.HasPushEvent, attribute.IsClassAttribute));

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
    }

    /// <summary>
    /// Data structure that details the mappings that can be performed from any supported connector.
    /// </summary>
    public class ConnectorMapping
    {
        /// <summary>
        /// Connector protocol name.
        /// </summary>
        public string ProtocolName { get; set; }

        /// <summary>
        /// Class mappings retrieved from the supported connector.
        /// </summary>
        public List<ClassMapping> ClassMappings { get; set; }

        /// <summary>
        /// Relationship details to build from the supported connector.
        /// </summary>
        public List<Relationship> Relationships { get; set; }

        /// <summary>
        /// ConnectorMapping class constructor.
        /// </summary>
        /// <param name="protocolName"></param>
        /// <param name="classMappings"></param>
        /// <param name="relationships"></param>
        public ConnectorMapping(string protocolName, List<ClassMapping> classMappings, List<Relationship> relationships)
        {
            ProtocolName = protocolName;
            ClassMappings = classMappings;
            Relationships = relationships;
        }
    }

    /// <summary>
    /// Data structure that details for each class mapping.
    /// </summary>
    public class ClassMapping
    {
        // TODO: Add support for CI discovery using External CI property values
        private Dictionary<string, Func<Engine, List<Property>, List<string>, string>> ciUniqueIdFunctionMapper;

        public Dictionary<string, Func<Engine, List<Property>, List<string>, string>> CiUniqueIdFunctionMapper
        {
            get
            {
                if (ciUniqueIdFunctionMapper != null)
                {
                    return ciUniqueIdFunctionMapper;
                }

                ciUniqueIdFunctionMapper = new Dictionary<string, Func<Engine, List<Property>, List<string>, string>>
                {
                    //  TODO: Add methods used to build CIs using custom methods
                    { "Dialog Satellite Network", GetDialogSatelliteNetworkUniqueID },
                    { "Dialog Linux Server", GetDialogLinuxServerUniqueID },
                    { "Dialog Switch", GetDialogSwitchUniqueID },
                    { "Dialog Modulator", GetDialogModulatorUniqueID },
                    { "Dialog Demodulator", GetDialogDemodulatorUniqueID },
                };

                return ciUniqueIdFunctionMapper;
            }
        }

        /// <summary>
        /// Class name.
        /// </summary>
        public string Class { get; set; }

        /// <summary>
        /// ServiceNow target table for the class.
        /// </summary>
        public string TargetTable { get; set; }

        /// <summary>
        /// Indicates if the class has a parent relationship to other connector classes.
        /// </summary>
        public bool IsParent { get; set; }

        /// <summary>
        /// Contains the details necessary to build unique IDs for CIs of a given class.
        /// </summary>
        public NamingDetails NamingDetails { get; set; }

        /// <summary>
        /// Specifies the details necessary to retrieve the mapped class attributes from the corresponding Dataminer elements.
        /// </summary>
        public Dictionary<int, List<ClassProperty>> AttributesByTableID { get; set; }

        /// <summary>
        /// Method used to retrieve supported connector table values that should be sent into the update solution push request.
        /// </summary>
        /// <param name="parameterValuesByPK"></param>
        /// <param name="pushParameters"></param>
        /// <param name="parameterIndices"></param>
        /// <param name="rowsByPK"></param>
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
                        searchedParameter = new ParameterDetails(parameter.AttributeName, parameter.Class, parameter.ParameterIdxByPid, parameter.IsMonitored, parameter.IsClassAttribute);

                        parameterValuesByPK[rowKvp.Key].Add(searchedParameter);
                    }

                    if (parameter.ParameterIdxByPid.Value == -1) continue;

                    int rowIdx = parameterIndices.FindIndex(x => x == (uint)parameter.ParameterIdxByPid.Value);

                    if (rowIdx == -1) continue;

                    searchedParameter.CurrentValue = Convert.ToString(rowKvp.Value[rowIdx]);
                }
            }
        }

        /// <summary>
        /// Method used to remove parameter values related to deprecated PKs.
        /// </summary>
        /// <param name="parameterValuesByPK"></param>
        /// <param name="currentPKs"></param>
        public static void RemoveDeprecatedParameterValueKeys(Dictionary<string, List<ParameterDetails>> parameterValuesByPK, List<string> currentPKs)
        {
            var deprecatedPKs = parameterValuesByPK.Keys.Where(pk => !currentPKs.Contains(pk)).ToList();

            foreach (var deprecatedPK in deprecatedPKs)
            {
                parameterValuesByPK.Remove(deprecatedPK);
            }
        }

        /// <summary>
        /// Method used to remove parameter values related to deprecated attributes.
        /// </summary>
        /// <param name="parameterValuesByPK"></param>
        /// <param name="pushParameters"></param>
        public static void RemoveDeprecatedAttributes(Dictionary<string, List<ParameterDetails>> parameterValuesByPK, List<ParameterDetails> pushParameters)
        {
            var pushParameterNames = pushParameters.Select(p => p.AttributeName).ToList();

            foreach (var parameterValuesKvp in parameterValuesByPK)
            {
                parameterValuesKvp.Value.RemoveAll(p => !pushParameterNames.Contains(p.AttributeName));
            }
        }

        /// <summary>
        /// Method used to retrieve monitored parameter updates to send via push request.
        /// </summary>
        /// <param name="parameterValuesByPK"></param>
        /// <param name="parameterUpdates"></param>
        /// <returns>Boolean indicating if there are parameter updates to send.</returns>
        public static bool TryGetParameterUpdatesToSend(Dictionary<string, List<ParameterDetails>> parameterValuesByPK, out Dictionary<string, List<ParameterDetails>> parameterUpdates)
        {
            parameterUpdates = new Dictionary<string, List<ParameterDetails>>();

            foreach (var parameterDetailsKvp in parameterValuesByPK)
            {
                foreach (var parameter in parameterDetailsKvp.Value)
                {
                    if (!parameter.CurrentValue.Equals(parameter.PreviousValue) || !parameter.IsMonitored)
                    {
                        if (parameterUpdates.ContainsKey(parameterDetailsKvp.Key))
                        {
                            parameterUpdates[parameterDetailsKvp.Key]
                                .Add(new ParameterDetails(parameter.AttributeName, parameter.Class, parameter.CurrentValue, parameter.IsMonitored, parameter.IsClassAttribute));
                        }
                        else
                        {
                            parameterUpdates.Add(parameterDetailsKvp.Key, new List<ParameterDetails>
                            {
                                new ParameterDetails(parameter.AttributeName, parameter.Class, parameter.CurrentValue, parameter.IsMonitored, parameter.IsClassAttribute),
                            });
                        }
                    }

                    //Clear parameter values for next polling cycle
                    parameter.PreviousValue = parameter.CurrentValue;
                    parameter.CurrentValue = String.Empty;
                }

                if (parameterUpdates.ContainsKey(parameterDetailsKvp.Key) && !parameterUpdates[parameterDetailsKvp.Key].Exists(u => u.IsMonitored))
                {
                    // If there are only parameter details used for naming remove
                    parameterUpdates.Remove(parameterDetailsKvp.Key);
                }
            }

            return parameterUpdates.Count > 0;
        }

        /// <summary>
        /// Method used to retrieve the unique ID of a given instance.
        /// </summary>
        /// <param name="engine"></param>
        /// <param name="properties"></param>
        /// <param name="parentElementName"></param>
        /// <param name="pk"></param>
        /// <returns>Instance unique ID.</returns>
        public string GetUniqueID(IEngine engine, string pk, List<Property> properties, string parentElementName)
        {
            if (String.IsNullOrWhiteSpace(parentElementName))
            {
                engine.GenerateInformation("GetUniqueID| Empty parent element name.");
                return String.Empty;
            }

            switch (NamingDetails.Format)
            {
                case NamingFormat.PrimaryKey:
                    {
                        return !String.IsNullOrWhiteSpace(pk) ? parentElementName + "." + pk : String.Empty;
                    }

                case NamingFormat.PrimaryKey_Label:
                    {
                        var labelProperty = properties.FirstOrDefault(x => x.Name.Equals("u_label"));

                        return labelProperty != null && !String.IsNullOrWhiteSpace(labelProperty.Value) ? parentElementName + "." + pk + "." + labelProperty.Value : String.Empty;
                    }

                case NamingFormat.Label:
                    {
                        var labelProperty = properties.FirstOrDefault(x => x.Name.Equals("u_label"));

                        return labelProperty != null && !String.IsNullOrWhiteSpace(labelProperty.Value) ? parentElementName + "." + labelProperty.Value : String.Empty;
                    }

                case NamingFormat.Label_PrimaryKey:
                    {
                        var labelProperty = properties.FirstOrDefault(x => x.Name.Equals("u_label"));

                        return labelProperty != null && !String.IsNullOrWhiteSpace(labelProperty.Value) ? parentElementName + "." + labelProperty.Value + "." + pk : String.Empty;
                    }

                case NamingFormat.Custom:
                    {
                        if (!CiUniqueIdFunctionMapper.ContainsKey(Class))
                        {
                            engine.GenerateInformation("GetUniqueID| Could not map custom method for '" + Class + "'.");
                            return String.Empty;
                        }

                        return CiUniqueIdFunctionMapper[Class].Invoke((Engine)engine, properties, new List<string> { parentElementName });
                    }

                default:
                    engine.GenerateInformation("GetUniqueID| Unsupported naming format.");
                    return String.Empty;
            }
        }

        /// <summary>
        /// Method used to retrieve the unique ID of a given instance.
        /// </summary>
        /// <param name="engine"></param>
        /// <param name="parentElementName"></param>
        /// <param name="pk"></param>
        /// <param name="classNamingFormat"></param>
        /// <param name="parameterDetails"></param>
        /// <returns>Instance unique ID.</returns>
        public string GetUniqueID(IEngine engine, string parentElementName, string pk, NamingFormat classNamingFormat, List<ParameterDetails> parameterDetails)
        {
            if (String.IsNullOrWhiteSpace(parentElementName))
            {
                engine.GenerateInformation("GetUniqueID| Empty parent element name.");
                return String.Empty;
            }

            switch (classNamingFormat)
            {
                case NamingFormat.PrimaryKey:
                    {
                        return !String.IsNullOrWhiteSpace(pk) ? parentElementName + "." + pk : String.Empty;
                    }

                case NamingFormat.PrimaryKey_Label:
                    {
                        var labelAttribute = parameterDetails.FirstOrDefault(x => x.AttributeName.Equals("u_label"));

                        return labelAttribute != null && !String.IsNullOrWhiteSpace(labelAttribute.CurrentValue) ? parentElementName + "." + pk + "." + labelAttribute.CurrentValue : String.Empty;
                    }

                case NamingFormat.Label:
                    {
                        var labelAttribute = parameterDetails.FirstOrDefault(x => x.AttributeName.Equals("u_label"));

                        return labelAttribute != null && !String.IsNullOrWhiteSpace(labelAttribute.CurrentValue) ? parentElementName + "." + labelAttribute.CurrentValue : String.Empty;
                    }

                case NamingFormat.Label_PrimaryKey:
                    {
                        var labelAttribute = parameterDetails.FirstOrDefault(x => x.AttributeName.Equals("u_label"));

                        return labelAttribute != null && !String.IsNullOrWhiteSpace(labelAttribute.CurrentValue) ? parentElementName + "." + labelAttribute.CurrentValue + "." + pk : String.Empty;
                    }

                case NamingFormat.Custom:
                    {
                        if (!CiUniqueIdFunctionMapper.ContainsKey(Class))
                        {
                            engine.GenerateInformation("GetUniqueID| Could not map custom method for '" + Class + "'.");
                            return String.Empty;
                        }

                        var properties = parameterDetails.Select(p => new Property(p.AttributeName, Class, p.CurrentValue)).ToList();

                        return CiUniqueIdFunctionMapper[Class].Invoke((Engine)engine, properties, new List<string> { parentElementName });
                    }

                default:
                    engine.GenerateInformation("GetUniqueID| Unsupported naming format.");
                    return String.Empty;
            }
        }

        private string GetDialogSatelliteNetworkUniqueID(Engine engine, List<Property> properties, List<string> additionalNamingComponents)
        {
            //engine.GenerateInformation("GetDialogSatelliteNetworkUniqueID| ********** Properties:\n\n" + JsonConvert.SerializeObject(properties) + "\n\n");

            if (additionalNamingComponents.Count == 0) return String.Empty;

            string parentElementName = additionalNamingComponents[0];

            var labelProperty = properties.FirstOrDefault(x => x.Name.Equals("u_label"));

            var hpsNameProperty = properties.FirstOrDefault(x => x.Name.Equals("u_hps_name"));

            return labelProperty != null && hpsNameProperty != null && !String.IsNullOrWhiteSpace(labelProperty.Value) && !String.IsNullOrWhiteSpace(hpsNameProperty.Value)
                ? parentElementName + "." + hpsNameProperty.Value + "." + labelProperty.Value : String.Empty;
        }

        private string GetDialogLinuxServerUniqueID(Engine engine, List<Property> properties, List<string> additionalNamingComponents)
        {
            //engine.GenerateInformation("GetDialogLinuxServerUniqueID| ********** Properties:\n\n" + JsonConvert.SerializeObject(properties) + "\n\n");

            if (additionalNamingComponents.Count == 0) return String.Empty;

            string parentElementName = additionalNamingComponents[0];

            var labelProperty = properties.FirstOrDefault(x => x.Name.Equals("u_label"));

            if (labelProperty == null || String.IsNullOrWhiteSpace(labelProperty.Value)) return String.Empty;

            var hubNameProperty = properties.FirstOrDefault(x => x.Name.Equals("u_hub_name"));

            if (hubNameProperty != null && !String.IsNullOrWhiteSpace(hubNameProperty.Value) && !hubNameProperty.Value.Equals("NA"))
            {
                return parentElementName + "." + hubNameProperty.Value.Replace(".ENC", String.Empty) + "." + labelProperty.Value ;
            }

            var hubNmsProperty = properties.FirstOrDefault(x => x.Name.Equals("u_hub_nms"));

            return hubNmsProperty != null && !String.IsNullOrWhiteSpace(hubNmsProperty.Value) && !hubNmsProperty.Value.Equals("NA")
                ? parentElementName + "." + hubNmsProperty.Value + "." + labelProperty.Value : String.Empty;
        }

        private string GetDialogModulatorUniqueID(Engine engine, List<Property> properties, List<string> additionalNamingComponents)
        {
            if (additionalNamingComponents.Count == 0) return String.Empty;

            string parentElementName = additionalNamingComponents[0];

            var labelProperty = properties.FirstOrDefault(x => x.Name.Equals("u_label"));

            if (labelProperty == null) return String.Empty;

            var deviceLabelProperty = properties.FirstOrDefault(x => x.Name.Equals("u_label_device"));

            //engine.GenerateInformation("GetDialogModulatorUniqueID| ********** Properties:\n\n" + JsonConvert.SerializeObject(properties) + "\n\n");

            if (deviceLabelProperty != null && !String.IsNullOrWhiteSpace(deviceLabelProperty.Value))
            {
                // Modulator XIF Workflow 
                var labelParts = deviceLabelProperty.Value.Split('.');

                if (labelParts.Length == 3)
                {
                    labelProperty.Value = deviceLabelProperty.Value;

                    return labelProperty.Value.Contains("MOD-") ? parentElementName + "." + labelProperty.Value : String.Empty;
                }
            }

            return GetUniqueIdForModulator4IF(engine, properties, parentElementName, labelProperty);
        }

        private static string GetUniqueIdForModulator4IF(Engine engine, List<Property> properties, string parentElementName, Property labelProperty)
        {
            var modulatorLabel = String.Empty;

            var mcm7500LabelProperty = properties.FirstOrDefault(x => x.Name.Equals("u_label_mcm7500"));
            var mcm6100LabelProperty = properties.FirstOrDefault(x => x.Name.Equals("u_label_m6100"));

            if (mcm7500LabelProperty != null && !String.IsNullOrWhiteSpace(mcm7500LabelProperty.Value))
            {
                modulatorLabel = mcm7500LabelProperty.Value;
            }

            if (mcm6100LabelProperty != null && !String.IsNullOrWhiteSpace(mcm6100LabelProperty.Value))
            {
                modulatorLabel = mcm6100LabelProperty.Value;
            }

            if (String.IsNullOrWhiteSpace(modulatorLabel)) return String.Empty;

            var modulatorLabelParts = modulatorLabel.Split('[');

            if (modulatorLabelParts.Length < 2) return String.Empty;

            modulatorLabel = modulatorLabelParts[0].Trim();

            labelProperty.Value = modulatorLabel;

            //engine.GenerateInformation("GetUniqueIdForModulator4IF| ********** Properties:\n\n" + JsonConvert.SerializeObject(properties) + "\n\n");

            return parentElementName + "." + labelProperty.Value;
        }

        private string GetDialogDemodulatorUniqueID(Engine engine, List<Property> properties, List<string> additionalNamingComponents)
        {
            //engine.GenerateInformation("GetDialogDemodulatorUniqueID| Properties:\n\n" + JsonConvert.SerializeObject(properties) + "\n\n");

            if (additionalNamingComponents.Count == 0) return String.Empty;

            string parentElementName = additionalNamingComponents[0];

            var labelProperty = properties.FirstOrDefault(x => x.Name.Equals("u_label"));

            return labelProperty != null && !String.IsNullOrWhiteSpace(labelProperty.Value) && !labelProperty.Value.Contains(".MOD-") ? parentElementName + "." + labelProperty.Value : String.Empty;
        }

        ///// <summary>
        ///// Method used to retrieve the unique ID of a given Dialog Switch instance.
        ///// </summary>
        ///// <param name="engine"></param>
        ///// <param name="properties"></param>
        ///// <param name="pk"></param>
        ///// <returns>Remote instance unique ID.</returns>
        private string GetDialogSwitchUniqueID(Engine engine, List<Property> properties, List<string> additionalNamingComponents)
        {
            engine.GenerateInformation("GetDialogSwitchUniqueID| Properties:\n\n" + JsonConvert.SerializeObject(properties.Select(x => x.Name)) + "\n\n");

            if (additionalNamingComponents.Count == 0) return String.Empty;

            string parentElementName = additionalNamingComponents[0];

            var labelProperty = properties.FirstOrDefault(x => x.Name.Equals("u_label"));

            if (labelProperty == null) return String.Empty;

            var accessSwitchLabelProperty = properties.FirstOrDefault(x => x.Name.Equals("u_label_access"));

            if (accessSwitchLabelProperty != null && !String.IsNullOrWhiteSpace(accessSwitchLabelProperty.Value))
            {
                labelProperty.Value = accessSwitchLabelProperty.Value;

                return parentElementName + "." + labelProperty.Value;
            }

            var rfSwitchLabelProperty = properties.FirstOrDefault(x => x.Name.Equals("u_label_rf"));

            if (rfSwitchLabelProperty != null && !String.IsNullOrWhiteSpace(rfSwitchLabelProperty.Value))
            {
                labelProperty.Value = rfSwitchLabelProperty.Value;

                return parentElementName + "." + labelProperty.Value;
            }

            return String.Empty;
        }
    }

    /// <summary>
    /// Class attribute details.
    /// </summary>
    public class ClassProperty
    {
        /// <summary>
        /// Class attribute name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Column index where the attribute value can be found..
        /// </summary>
        public int ColumnIdx { get; set; }

        /// <summary>
        /// Indicates if the attribute is monitored and the updated value sent by Push event.
        /// </summary>
        public bool HasPushEvent { get; set; }

        /// <summary>
        /// Indicates if a certain attribute is ServiceNow class attribute.
        /// </summary>
        public bool IsClassAttribute { get; set; }

        /// <summary>
        /// Indicates if the attribute is used to build the unique ID for CIs.
        /// </summary>
        public bool IsNamingProperty { get; set; }

        /// <summary>
        /// ClassAttribute class constructor.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="columnIdx"></param>
        /// <param name="hasPushEvent"></param>
        /// <param name="isClassAttribute"></param>
        /// <param name="isNamingProperty"></param>
        public ClassProperty(string name, int columnIdx, bool hasPushEvent, bool isClassAttribute, bool isNamingProperty)
        {
            Name = name;
            ColumnIdx = columnIdx;
            HasPushEvent = hasPushEvent;
            IsClassAttribute = isClassAttribute;
            IsNamingProperty = isNamingProperty;
        }
    }

    /// <summary>
    /// Property details
    /// </summary>
    public class Property
    {
        /// <summary>
        /// Possible values for the state property.
        /// </summary>
        public enum State
        {
            Unknown = -99,
            Deactivated = 0,
            Active,
            Removed,
            Decomissioned,
        }

        /// <summary>
        /// Property name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Class of the CI to which the property belongs.
        /// </summary>
        public string Class { get; set; }

        /// <summary>
        /// Property value.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Property class constructor.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="className"></param>
        /// <param name="value"></param>
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

        /// <summary>
        /// Method used to retrieve the status value depending on the value type provided.
        /// </summary>
        /// <param name="status"></param>
        /// <returns>Proper status value.</returns>
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

    /// <summary>
    /// Contains the relationship details.
    /// </summary>
    public class Relationship
    {
        /// <summary>
        /// Relationship name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Class of the relationship parent CI.
        /// </summary>
        public string ParentClass { get; set; }

        /// <summary>
        /// Class of the relationship child CI.
        /// </summary>
        public string ChildClass { get; set; }

        /// <summary>
        /// Describes the internal property requirements necessary to build a given relationship.
        /// </summary>
        public List<PropertyLink> InternalProperties { get; set; }

        /// <summary>
        /// Describes the external property requirements necessary to build a given relationship.
        /// </summary>
        public List<PropertyLink> ExternalProperties { get; set; }

        /// <summary>
        /// Relationship type/description.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Indicates if the relationship mapping should be performed from property values contained in the parent CI.
        /// </summary>
        public bool IsMappedFromParent { get; set; }

        /// <summary>
        /// Relationship class constructor.
        /// </summary>
        /// <param name="childClass"></param>
        /// <param name="parentClass"></param>
        /// <param name="internalProperties"></param>
        /// <param name="externalProperties"></param>
        /// <param name="type"></param>
        /// <param name="isMappedFromParent"></param>
        public Relationship(string childClass, string parentClass, List<PropertyLink> internalProperties, List<PropertyLink> externalProperties, string type, bool isMappedFromParent)
        {
            Name = childClass + "/" + type + "/" + parentClass;
            ParentClass = parentClass;
            ChildClass = childClass;
            InternalProperties = internalProperties;
            ExternalProperties = externalProperties;
            Type = type;
            IsMappedFromParent = isMappedFromParent;
        }
    }

    /// <summary>
    /// Describes how a given property is used to link property values between CIs.
    /// </summary>
    public class PropertyLink
    {
        /// <summary>
        /// Contains the details of a property that belongs to a child CI, but is needed to make a certain data connection.
        /// </summary>
        public Property ChildProperty { get; set; }

        /// <summary>
        /// Contains the details of a property that belongs to a parent CI, but is needed to make a certain data connection.
        /// </summary>
        public Property ParentProperty { get; set; }

        /// <summary>
        /// ExternalPropertyLink default class constructor.
        /// </summary>
        public PropertyLink()
        {
            ChildProperty = new Property(String.Empty, String.Empty, String.Empty);
            ParentProperty = new Property(String.Empty, String.Empty, String.Empty);
        }

        /// <summary>
        /// PropertyLink class constructor.
        /// </summary>
        /// <param name="childProperty"></param>
        /// <param name="parentProperty"></param>
        public PropertyLink(string childProperty, string parentProperty)
        {
            ChildProperty = new Property(childProperty, String.Empty, String.Empty);
            ParentProperty = new Property(parentProperty, String.Empty, String.Empty);
        }

        /// <summary>
        /// PropertyLink class constructor.
        /// </summary>
        /// <param name="childProperty"></param>
        /// <param name="parentProperty"></param>
        /// <param name="childClass"></param>
        /// <param name="parentClass"></param>
        public PropertyLink(string childProperty, string parentProperty, string childClass, string parentClass)
        {
            ChildProperty = new Property(childProperty, childClass, String.Empty);
            ParentProperty = new Property(parentProperty, parentClass, String.Empty);
        }
    }

    /// <summary>
    /// Contains the details of a monitored parameter.
    /// </summary>
    public class ParameterDetails
    {
        /// <summary>
        /// Name of the attribute.
        /// </summary>
        public string AttributeName { get; set; }

        /// <summary>
        /// Class of the attribute
        /// </summary>
        public string Class { get; set; }

        /// <summary>
        /// KeyValuePair containing info on how to retrieve a given parameter from a supported connector.
        /// </summary>
        public KeyValuePair<int, int> ParameterIdxByPid { get; set; }

        /// <summary>
        /// Current value of the monitored parameter.
        /// </summary>
        public string CurrentValue { get; set; }

        /// <summary>
        /// Previous value of the monitored parameter.
        /// </summary>
        public string PreviousValue { get; set; }

        /// <summary>
        /// Indicates if this is a monitored parameter.
        /// </summary>
        public bool IsMonitored { get; set; }

        /// <summary>
        /// Indicates if a certain attribute is ServiceNow class attribute.
        /// </summary>
        public bool IsClassAttribute { get; set; }

        /// <summary>
        /// ParameterDetails class constructor.
        /// </summary>
        /// <param name="attributeName"></param>
        /// <param name="className"></param>
        /// <param name="paramIdxByPid"></param>
        /// <param name="currentValue"></param>
        /// <param name="previousValue"></param>
        /// <param name="isMonitored"></param>
        /// <param name="isClassAttribute"></param>
        [JsonConstructor]
        public ParameterDetails(string attributeName, string className, KeyValuePair<int, int> paramIdxByPid, string currentValue, string previousValue, bool isMonitored, bool isClassAttribute)
        {
            AttributeName = attributeName;
            Class = className;
            ParameterIdxByPid = paramIdxByPid;
            CurrentValue = currentValue;
            PreviousValue = previousValue;
            IsMonitored = isMonitored;
            IsClassAttribute = isClassAttribute;
        }

        /// <summary>
        /// ParameterDetails class constructor.
        /// </summary>
        /// <param name="attributeName"></param>
        /// <param name="className"></param>
        /// <param name="paramIdxByPid"></param>
        /// <param name="isMonitored"></param>
        /// <param name="isClassAttribute"></param>
        public ParameterDetails(string attributeName, string className, KeyValuePair<int, int> paramIdxByPid, bool isMonitored, bool isClassAttribute)
        {
            AttributeName = attributeName;
            Class = className;
            ParameterIdxByPid = paramIdxByPid;
            CurrentValue = String.Empty;
            PreviousValue = String.Empty;
            IsMonitored = isMonitored;
            IsClassAttribute = isClassAttribute;
        }

        /// <summary>
        /// ParameterDetails class constructor.
        /// </summary>
        /// <param name="attributeName"></param>
        /// <param name="className"></param>
        /// <param name="currentValue"></param>
        /// <param name="isMonitored"></param>
        /// <param name="isClassAttribute"></param>
        public ParameterDetails(string attributeName, string className, string currentValue, bool isMonitored, bool isClassAttribute)
        {
            AttributeName = attributeName;
            Class = className;
            ParameterIdxByPid = new KeyValuePair<int, int>();
            CurrentValue = currentValue;
            PreviousValue = String.Empty;
            IsMonitored = isMonitored;
            IsClassAttribute = isClassAttribute;
        }
    }

    /// <summary>
    /// Contains the details necessary to build a CI unique ID.
    /// </summary>
    public class NamingDetails
    {
        /// <summary>
        /// Unique ID naming format.
        /// </summary>
        public NamingFormat Format { get; set; }

        /// <summary>
        /// Describes the external property requirements necessary to build a certain unique ID.
        /// </summary>
        public PropertyLink ExternalPropertyLink { get; set; }

        /// <summary>
        /// NamingDetails default class constructor.
        /// </summary>
        public NamingDetails()
        {
            Format = NamingFormat.Unknown;
            ExternalPropertyLink = new PropertyLink();
        }

        /// <summary>
        /// NamingDetails class constructor.
        /// </summary>
        /// <param name="format"></param>
        /// <param name="externalPropertyLink"></param>
        public NamingDetails(NamingFormat format, PropertyLink externalPropertyLink)
        {
            Format = format;
            ExternalPropertyLink = externalPropertyLink;
        }
    }
}
