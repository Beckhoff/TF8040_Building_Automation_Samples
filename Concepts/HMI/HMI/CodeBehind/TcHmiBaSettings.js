(function (TcHmi) {
    TcHmi.EventProvider.register(TcHmi.BuildingAutomation.TcHmiBaEvents.onOverrideSettings, function (e, data) {
        e.destroy();

        /**********************************************************************************************************************************/
        /************************************************************ Controls ************************************************************/
        /**********************************************************************************************************************************/

        /*****************************************************************/
        /*************************** EventList ***************************/

        TcHmi.BuildingAutomation.Controls.Management.EventList.DefaultColumns = [
            { type: TcHmi.BuildingAutomation.Controls.Management.EventList.Columns.baIdentifier, width: 30, widthUnit: 'px' },
            { type: TcHmi.BuildingAutomation.Controls.Management.EventList.Columns.event, width: 30, widthUnit: 'px' },
            { type: TcHmi.BuildingAutomation.Controls.Management.EventList.Columns.timestamp, width: 140, widthUnit: 'px' },
            { type: TcHmi.BuildingAutomation.Controls.Management.EventList.Columns.device, width: 70, widthUnit: 'px' },
            { type: TcHmi.BuildingAutomation.Controls.Management.EventList.Columns.objectName, width: 300, widthUnit: 'px' },
            { type: TcHmi.BuildingAutomation.Controls.Management.EventList.Columns.instancePath, width: 300, widthUnit: 'px' },
            { type: TcHmi.BuildingAutomation.Controls.Management.EventList.Columns.description, width: 300, widthUnit: 'px' },
            { type: TcHmi.BuildingAutomation.Controls.Management.EventList.Columns.eventClass, width: 30, widthUnit: 'px' },
            { type: TcHmi.BuildingAutomation.Controls.Management.EventList.Columns.eventClassInstanceDescription, width: 200, widthUnit: 'px' }
        ];

        TcHmi.BuildingAutomation.Controls.Management.EventList.MaximumEventTypePulse = new Map([
            [TcHmi.BuildingAutomation.BA.Role.eGuest, TcHmi.BuildingAutomation.BA.EventType.eDisturb],
            [TcHmi.BuildingAutomation.BA.Role.eBasic, TcHmi.BuildingAutomation.BA.EventType.eDisturb],
            [TcHmi.BuildingAutomation.BA.Role.eAdvanced, TcHmi.BuildingAutomation.BA.EventType.eDisturb],
            [TcHmi.BuildingAutomation.BA.Role.eExpert, TcHmi.BuildingAutomation.BA.EventType.eDisturb],
            [TcHmi.BuildingAutomation.BA.Role.eInternal, TcHmi.BuildingAutomation.BA.EventType.eDisturb]
        ]);

        /*****************************************************************/
        /******************** ProjectNavigationTextual *******************/

        TcHmi.BuildingAutomation.Controls.Management.ProjectNavigationTextual.DefaultContentViewDialogWidth = 1000;

        /*****************************************************************/
        /************************* RoomAutomation ************************/

        TcHmi.BuildingAutomation.Controls.RoomAutomation.RoomControl.DefaultBaObjectListDialogWidth = 1000;

        TcHmi.BuildingAutomation.Controls.RoomAutomation.Light.ShowQuickLinks = false;
        TcHmi.BuildingAutomation.Controls.RoomAutomation.Light.PriorityIcons = new Map([
            [TcHmi.BuildingAutomation.Controls.RoomAutomation.Light.Priority.fire, TcHmi.BuildingAutomation.Icons.convertIIconDataToIIconAttributes({ ...TcHmi.BuildingAutomation.Icons.Building.FireAlarm, color: TcHmi.BuildingAutomation.Color.RGBAColor.Red })],
            [TcHmi.BuildingAutomation.Controls.RoomAutomation.Light.Priority.communicationError, TcHmi.BuildingAutomation.Icons.convertIIconDataToIIconAttributes({ ...TcHmi.BuildingAutomation.Icons.Events.Flag.Fault, color: TcHmi.BuildingAutomation.Color.RGBAColor.Red })],
            [TcHmi.BuildingAutomation.Controls.RoomAutomation.Light.Priority.burglary, TcHmi.BuildingAutomation.Icons.convertIIconDataToIIconAttributes({ ...TcHmi.BuildingAutomation.Icons.Building.BurglarAlarm, color: TcHmi.BuildingAutomation.Color.RGBAColor.Red })],
            [TcHmi.BuildingAutomation.Controls.RoomAutomation.Light.Priority.maintenance, TcHmi.BuildingAutomation.Icons.convertIIconDataToIIconAttributes({ ...TcHmi.BuildingAutomation.Icons.Building.Maintenance, color: TcHmi.BuildingAutomation.Color.RGBAColor.DarkOrange })],
            [TcHmi.BuildingAutomation.Controls.RoomAutomation.Light.Priority.cleaning, TcHmi.BuildingAutomation.Icons.convertIIconDataToIIconAttributes({ ...TcHmi.BuildingAutomation.Icons.Building.Cleaning, color: TcHmi.BuildingAutomation.Color.RGBAColor.Yellow })],
            [TcHmi.BuildingAutomation.Controls.RoomAutomation.Light.Priority.nightWatch, TcHmi.BuildingAutomation.Icons.convertIIconDataToIIconAttributes({ ...TcHmi.BuildingAutomation.Icons.Building.NightWatch, color: TcHmi.BuildingAutomation.Color.RGBAColor.Blue })],
            [TcHmi.BuildingAutomation.Controls.RoomAutomation.Light.Priority.manual, TcHmi.BuildingAutomation.Icons.convertIIconDataToIIconAttributes({ ...TcHmi.BuildingAutomation.Icons.RoomAutomation.Manual, color: TcHmi.BuildingAutomation.Color.RGBAColor.DarkOrange })],
            [TcHmi.BuildingAutomation.Controls.RoomAutomation.Light.Priority.automaticLight, TcHmi.BuildingAutomation.Icons.convertIIconDataToIIconAttributes({ ...TcHmi.BuildingAutomation.Icons.RoomAutomation.Automatic, color: TcHmi.BuildingAutomation.Color.RGBAColor.TcHmiGreen })]
        ]);

        TcHmi.BuildingAutomation.Controls.RoomAutomation.Sunblind.PriorityIcons = new Map([
            [TcHmi.BuildingAutomation.Controls.RoomAutomation.Sunblind.Priority.fire, TcHmi.BuildingAutomation.Icons.convertIIconDataToIIconAttributes({ ...TcHmi.BuildingAutomation.Icons.Building.FireAlarm, color: TcHmi.BuildingAutomation.Color.RGBAColor.Red })],
            [TcHmi.BuildingAutomation.Controls.RoomAutomation.Sunblind.Priority.storm, TcHmi.BuildingAutomation.Icons.convertIIconDataToIIconAttributes({ ...TcHmi.BuildingAutomation.Icons.Building.StormAlarm, color: TcHmi.BuildingAutomation.Color.RGBAColor.Blue })],
            [TcHmi.BuildingAutomation.Controls.RoomAutomation.Sunblind.Priority.ice, TcHmi.BuildingAutomation.Icons.convertIIconDataToIIconAttributes({ ...TcHmi.BuildingAutomation.Icons.Building.IceAlarm, color: TcHmi.BuildingAutomation.Color.RGBAColor.Blue })],
            [TcHmi.BuildingAutomation.Controls.RoomAutomation.Sunblind.Priority.commError, TcHmi.BuildingAutomation.Icons.convertIIconDataToIIconAttributes({ ...TcHmi.BuildingAutomation.Icons.Events.Flag.Fault, color: TcHmi.BuildingAutomation.Color.RGBAColor.Red })],
            [TcHmi.BuildingAutomation.Controls.RoomAutomation.Sunblind.Priority.burglary, TcHmi.BuildingAutomation.Icons.convertIIconDataToIIconAttributes({ ...TcHmi.BuildingAutomation.Icons.Building.BurglarAlarm, color: TcHmi.BuildingAutomation.Color.RGBAColor.Red })],
            [TcHmi.BuildingAutomation.Controls.RoomAutomation.Sunblind.Priority.maintenance, TcHmi.BuildingAutomation.Icons.convertIIconDataToIIconAttributes({ ...TcHmi.BuildingAutomation.Icons.Building.Maintenance, color: TcHmi.BuildingAutomation.Color.RGBAColor.DarkOrange })],
            [TcHmi.BuildingAutomation.Controls.RoomAutomation.Sunblind.Priority.manualActuator, TcHmi.BuildingAutomation.Icons.convertIIconDataToIIconAttributes({ ...TcHmi.BuildingAutomation.Icons.RoomAutomation.Manual, color: TcHmi.BuildingAutomation.Color.RGBAColor.DarkOrange })],
            [TcHmi.BuildingAutomation.Controls.RoomAutomation.Sunblind.Priority.manualGroup, TcHmi.BuildingAutomation.Icons.convertIIconDataToIIconAttributes({ ...TcHmi.BuildingAutomation.Icons.RoomAutomation.Manual, color: TcHmi.BuildingAutomation.Color.RGBAColor.DarkOrange })],
            [TcHmi.BuildingAutomation.Controls.RoomAutomation.Sunblind.Priority.facadeThermoAutomatic, TcHmi.BuildingAutomation.Icons.convertIIconDataToIIconAttributes({ ...TcHmi.BuildingAutomation.Icons.RoomAutomation.Automatic, color: TcHmi.BuildingAutomation.Color.RGBAColor.TcHmiGreen })],
            [TcHmi.BuildingAutomation.Controls.RoomAutomation.Sunblind.Priority.facadeTwilightAutomatic, TcHmi.BuildingAutomation.Icons.convertIIconDataToIIconAttributes({ ...TcHmi.BuildingAutomation.Icons.RoomAutomation.Automatic, color: TcHmi.BuildingAutomation.Color.RGBAColor.TcHmiGreen })],
            [TcHmi.BuildingAutomation.Controls.RoomAutomation.Sunblind.Priority.sunProtection, TcHmi.BuildingAutomation.Icons.convertIIconDataToIIconAttributes({ ...TcHmi.BuildingAutomation.Icons.RoomAutomation.Automatic, color: TcHmi.BuildingAutomation.Color.RGBAColor.TcHmiGreen })]
        ]);

        TcHmi.BuildingAutomation.Controls.RoomAutomation.Window.ShowQuickLinks = false;

        /**********************************************************************************************************************************/
        /*********************************************************** Framework ************************************************************/
        /**********************************************************************************************************************************/

        /*****************************************************************/
        /************************** BusyHandler **************************/

        TcHmi.BuildingAutomation.BusyHandler.RecordTimerResults = false;

        /*****************************************************************/
        /**************************** Button *****************************/

        TcHmi.BuildingAutomation.Components.Button.DoublePressDuration = 200;

        /*****************************************************************/
        /*************************** Calendar ****************************/

        TcHmi.BuildingAutomation.Components.Calendar.DefaultEventDialogSize = {
            width: 700,
            height: 350
        };

        /*****************************************************************/
        /************************* Calendar List *************************/

        TcHmi.BuildingAutomation.Components.CalendarList.DefaultCalendarListDialogSize = {
            width: 750,
            height: 400
        };

        /*****************************************************************/
        /************************* ContentWindow *************************/

        TcHmi.BuildingAutomation.Components.ContentWindow.HideHeaderIconBorderDefault = false;

        /*****************************************************************/
        /************************* DateTimePicker ************************/

        TcHmi.BuildingAutomation.Components.DateTimePicker.DefaultDateTimePickerDialogSize = {
            width: 650,
            height: 450
        };
        TcHmi.BuildingAutomation.Components.DateTimePicker.DefaultDatePickerDialogSize = {
            width: 300,
            height: 300
        };

        /*****************************************************************/
        /************************** DialogWindow **************************/

        TcHmi.BuildingAutomation.Components.DialogWindow.MoveLimitation = 100;

        /*****************************************************************/
        /*************************** TimePicker **************************/

        TcHmi.BuildingAutomation.Components.TimePicker.DefaultTimePickerDialogSize = {
            width: 350,
            height: 200
        };

        /*****************************************************************/
        /***************************** UiIcon ****************************/

        TcHmi.BuildingAutomation.Components.UiIcon.AutoActivateIconStatus = false;
        TcHmi.BuildingAutomation.Components.UiIcon.EnableEventCountBadge = true;
        TcHmi.BuildingAutomation.Components.UiIcon.AutoScaleEventIconThreshold = 40;
        TcHmi.BuildingAutomation.Components.UiIcon.EventIconSize = 30;

        TcHmi.BuildingAutomation.Components.UiIcon.MaximumEventConditionDisplayed = new Map([
            [TcHmi.BuildingAutomation.BA.Role.eGuest, TcHmi.BuildingAutomation.BA.EventCondition.eTypeOther],
            [TcHmi.BuildingAutomation.BA.Role.eBasic, TcHmi.BuildingAutomation.BA.EventCondition.eTypeOther],
            [TcHmi.BuildingAutomation.BA.Role.eAdvanced, TcHmi.BuildingAutomation.BA.EventCondition.eEventIconDisplayed],
            [TcHmi.BuildingAutomation.BA.Role.eExpert, TcHmi.BuildingAutomation.BA.EventCondition.eEventIconDisplayed],
            [TcHmi.BuildingAutomation.BA.Role.eInternal, TcHmi.BuildingAutomation.BA.EventCondition.eEventIconDisplayed]
        ]);

        TcHmi.BuildingAutomation.Components.UiIcon.MaximumEventTypePulse = new Map([
            [TcHmi.BuildingAutomation.BA.Role.eGuest, TcHmi.BuildingAutomation.BA.EventType.eDisturb],
            [TcHmi.BuildingAutomation.BA.Role.eBasic, TcHmi.BuildingAutomation.BA.EventType.eDisturb],
            [TcHmi.BuildingAutomation.BA.Role.eAdvanced, TcHmi.BuildingAutomation.BA.EventType.eDisturb],
            [TcHmi.BuildingAutomation.BA.Role.eExpert, TcHmi.BuildingAutomation.BA.EventType.eDisturb],
            [TcHmi.BuildingAutomation.BA.Role.eInternal, TcHmi.BuildingAutomation.BA.EventType.eDisturb]
        ]);

        /**********************************************************************************************************************************/
        /************************************************************* BaSite *************************************************************/
        /**********************************************************************************************************************************/

        TcHmi.BuildingAutomation.Server.BaSite.DefaultDiagnosticsDialogSize = {
            height: 500,
            width: 500
        };

        /**********************************************************************************************************************************/
        /************************************************************* General ************************************************************/
        /**********************************************************************************************************************************/

        /*****************************************************************/
        /************************* BaBasicObject *************************/

        TcHmi.BuildingAutomation.BA.BaBasicObject.AutoCollapseParameterCategories = false;
        TcHmi.BuildingAutomation.BA.BaBasicObject.DisableParameterDialogHeaderIcon = false;
        TcHmi.BuildingAutomation.BA.BaBasicObject.AutoCollapseNavigationDialogEntries = true;
        TcHmi.BuildingAutomation.BA.BaBasicObject.DefaultParameterDialogWidth = 800;
        TcHmi.BuildingAutomation.BA.BaBasicObject.DefaultNavigationDialogWidth = 1000;
        TcHmi.BuildingAutomation.BA.BaBasicObject.DefaultOnlineTrendDialogSize = {
            height: 250,
            width: 450
        };

        /*****************************************************************/
        /**************************** BaDevice ***************************/

        TcHmi.BuildingAutomation.BA.BaDevice.DialogConnectionAutoCloseStateChanged = true;
        TcHmi.BuildingAutomation.BA.BaDevice.DialogConnectionAutoReloadOnReconnect = true;
        TcHmi.BuildingAutomation.BA.BaDevice.DialogConnectionAutoReloadOnReconnectTime = 30;

        /*****************************************************************/
        /***************************** BaView ****************************/

        TcHmi.BuildingAutomation.BA.BaView.DisableNodeTypeIcons = false;

        /*****************************************************************/
        /**************************** Charting ***************************/

        TcHmi.BuildingAutomation.Charting.Axis.DefaultAxisOptionsDialogSize = {
            height: 180,
            width: 350
        };

        TcHmi.BuildingAutomation.Charting.Trend.DefaultDisplayedObjects = TcHmi.BuildingAutomation.Charting.Trend.DisplayedObjects.trendableObjects;
        TcHmi.BuildingAutomation.Charting.Trend.DefaultDisplayedDescription = TcHmi.BuildingAutomation.BA.BaParameterId.eInstDescription;

        TcHmi.BuildingAutomation.Charting.Trend.CollectionConfigurator.DefaultConfiguratorDialogWidth = 1000;
        TcHmi.BuildingAutomation.Charting.Trend.CollectionConfigurator.DefaultTrendCollectionSelectionDialogWidth = 400;

        /*****************************************************************/
        /************************** EventHelper **************************/

        TcHmi.BuildingAutomation.BA.EventHelper.MaximumEventCondition = new Map([
            [TcHmi.BuildingAutomation.BA.Role.eGuest, TcHmi.BuildingAutomation.BA.EventCondition.eEventIconDisplayed],
            [TcHmi.BuildingAutomation.BA.Role.eBasic, TcHmi.BuildingAutomation.BA.EventCondition.eEventIconDisplayed],
            [TcHmi.BuildingAutomation.BA.Role.eAdvanced, TcHmi.BuildingAutomation.BA.EventCondition.eEventIconDisplayed],
            [TcHmi.BuildingAutomation.BA.Role.eExpert, TcHmi.BuildingAutomation.BA.EventCondition.eEventIconDisplayed],
            [TcHmi.BuildingAutomation.BA.Role.eInternal, TcHmi.BuildingAutomation.BA.EventCondition.eEventIconDisplayed]
        ]);

        /*****************************************************************/
        /*************************** Parameter ***************************/

        TcHmi.BuildingAutomation.BA.HiddenBaParameterIds = [];
        TcHmi.BuildingAutomation.BA.BaParameterCategories = new Map([
            [TcHmi.BuildingAutomation.BA.BaParameterCategory.addressing, [
                TcHmi.BuildingAutomation.BA.BaParameterId.eInstanceID,
                TcHmi.BuildingAutomation.BA.BaParameterId.eObjectName,
                TcHmi.BuildingAutomation.BA.BaParameterId.eObjectType,
                TcHmi.BuildingAutomation.BA.BaParameterId.eDescription,
                TcHmi.BuildingAutomation.BA.BaParameterId.eSymbolPath,
                TcHmi.BuildingAutomation.BA.BaParameterId.eSymbolName,
                TcHmi.BuildingAutomation.BA.BaParameterId.eInstObjectName,
                TcHmi.BuildingAutomation.BA.BaParameterId.eInstDescription,
                TcHmi.BuildingAutomation.BA.BaParameterId.eNodeType,
                TcHmi.BuildingAutomation.BA.BaParameterId.eTag
            ]],
            [TcHmi.BuildingAutomation.BA.BaParameterCategory.dataExchange, [
                TcHmi.BuildingAutomation.BA.BaParameterId.ePresentValue,
                TcHmi.BuildingAutomation.BA.BaParameterId.eDeviceType,
                TcHmi.BuildingAutomation.BA.BaParameterId.eUnit,
                TcHmi.BuildingAutomation.BA.BaParameterId.eOutOfService,
                TcHmi.BuildingAutomation.BA.BaParameterId.eResolution,
                TcHmi.BuildingAutomation.BA.BaParameterId.eScaleOffset,
                TcHmi.BuildingAutomation.BA.BaParameterId.eCovIncrement,
                TcHmi.BuildingAutomation.BA.BaParameterId.eInactiveText,
                TcHmi.BuildingAutomation.BA.BaParameterId.eActiveText,
                TcHmi.BuildingAutomation.BA.BaParameterId.ePolarity,
                TcHmi.BuildingAutomation.BA.BaParameterId.eStateText,
                TcHmi.BuildingAutomation.BA.BaParameterId.eStateCount,
                TcHmi.BuildingAutomation.BA.BaParameterId.eActivePriority,
                TcHmi.BuildingAutomation.BA.BaParameterId.ePriorityArray,
                TcHmi.BuildingAutomation.BA.BaParameterId.eDefaultValue,
                TcHmi.BuildingAutomation.BA.BaParameterId.eEnManualRm,
                TcHmi.BuildingAutomation.BA.BaParameterId.eValManualRm,
                TcHmi.BuildingAutomation.BA.BaParameterId.eDampConstant
            ]],
            [TcHmi.BuildingAutomation.BA.BaParameterCategory.configuration, [
                TcHmi.BuildingAutomation.BA.BaParameterId.eConfigurate,
                TcHmi.BuildingAutomation.BA.BaParameterId.eToggleMode,
                TcHmi.BuildingAutomation.BA.BaParameterId.eStepDelay,
                TcHmi.BuildingAutomation.BA.BaParameterId.eMinOffTime,
                TcHmi.BuildingAutomation.BA.BaParameterId.eMinOnTime
            ]],
            [TcHmi.BuildingAutomation.BA.BaParameterCategory.operationalData, [
                TcHmi.BuildingAutomation.BA.BaParameterId.eStateChangeTime,
                TcHmi.BuildingAutomation.BA.BaParameterId.eStateChangeCount,
                TcHmi.BuildingAutomation.BA.BaParameterId.eStateChangeResetPoint,
                TcHmi.BuildingAutomation.BA.BaParameterId.eActiveTimeElapsed,
                TcHmi.BuildingAutomation.BA.BaParameterId.eActiveTimeResetPoint,
                TcHmi.BuildingAutomation.BA.BaParameterId.eReliability
            ]],
            [TcHmi.BuildingAutomation.BA.BaParameterCategory.eventManagement, [
                TcHmi.BuildingAutomation.BA.BaParameterId.eActiveEvent,
                TcHmi.BuildingAutomation.BA.BaParameterId.eEventState,
                TcHmi.BuildingAutomation.BA.BaParameterId.eStatusFlags,
                TcHmi.BuildingAutomation.BA.BaParameterId.eEventClassID,
                TcHmi.BuildingAutomation.BA.BaParameterId.eEventDetectionEnable,
                TcHmi.BuildingAutomation.BA.BaParameterId.eEventEnable,
                TcHmi.BuildingAutomation.BA.BaParameterId.eEventMessage,
                TcHmi.BuildingAutomation.BA.BaParameterId.eTimeDelay,
                TcHmi.BuildingAutomation.BA.BaParameterId.eEventAlgorithmInhibit,
                TcHmi.BuildingAutomation.BA.BaParameterId.eLowLimit,
                TcHmi.BuildingAutomation.BA.BaParameterId.eHighLimit,
                TcHmi.BuildingAutomation.BA.BaParameterId.eAlarmValue,
                TcHmi.BuildingAutomation.BA.BaParameterId.eAlarmValues,
                TcHmi.BuildingAutomation.BA.BaParameterId.eLimitDeadband,
                TcHmi.BuildingAutomation.BA.BaParameterId.eEventMessageFormat,
                TcHmi.BuildingAutomation.BA.BaParameterId.eEventTransitionText,
                TcHmi.BuildingAutomation.BA.BaParameterId.eEventChangeTime,
                TcHmi.BuildingAutomation.BA.BaParameterId.eAckedTransitions,
                TcHmi.BuildingAutomation.BA.BaParameterId.eEventType,
                TcHmi.BuildingAutomation.BA.BaParameterId.ePriority,
                TcHmi.BuildingAutomation.BA.BaParameterId.eAlarmMode,
                TcHmi.BuildingAutomation.BA.BaParameterId.eAcknowledgeRm,
                TcHmi.BuildingAutomation.BA.BaParameterId.eAcknowledgeRequired,
                TcHmi.BuildingAutomation.BA.BaParameterId.eEnPlantLock
            ]],
            [TcHmi.BuildingAutomation.BA.BaParameterCategory.trending, [
                TcHmi.BuildingAutomation.BA.BaParameterId.eEnable,
                TcHmi.BuildingAutomation.BA.BaParameterId.eLoggingType,
                TcHmi.BuildingAutomation.BA.BaParameterId.eStopOnFull,
                TcHmi.BuildingAutomation.BA.BaParameterId.eNotificationThreshold,
                TcHmi.BuildingAutomation.BA.BaParameterId.eLogInterval,
                TcHmi.BuildingAutomation.BA.BaParameterId.eStartTime,
                TcHmi.BuildingAutomation.BA.BaParameterId.eStopTime,
                TcHmi.BuildingAutomation.BA.BaParameterId.eBufferSize,
                TcHmi.BuildingAutomation.BA.BaParameterId.eLogBuffer,
                TcHmi.BuildingAutomation.BA.BaParameterId.eAssignAsTrendReference,
                TcHmi.BuildingAutomation.BA.BaParameterId.eRecordCount,
                TcHmi.BuildingAutomation.BA.BaParameterId.eTotalRecordCount
            ]],
            [TcHmi.BuildingAutomation.BA.BaParameterCategory.scheduling, [
                TcHmi.BuildingAutomation.BA.BaParameterId.eWeek,
                TcHmi.BuildingAutomation.BA.BaParameterId.eCalendar,
                TcHmi.BuildingAutomation.BA.BaParameterId.eDateList,
                TcHmi.BuildingAutomation.BA.BaParameterId.eException
            ]],
            [TcHmi.BuildingAutomation.BA.BaParameterCategory.controlling, [
                TcHmi.BuildingAutomation.BA.BaParameterId.eSetpoint,
                TcHmi.BuildingAutomation.BA.BaParameterId.eControlledValue,
                TcHmi.BuildingAutomation.BA.BaParameterId.eCtrlDeviation,
                TcHmi.BuildingAutomation.BA.BaParameterId.eMinOutput,
                TcHmi.BuildingAutomation.BA.BaParameterId.eMaxOutput,
                TcHmi.BuildingAutomation.BA.BaParameterId.eOutputUnit,
                TcHmi.BuildingAutomation.BA.BaParameterId.eAction,
                TcHmi.BuildingAutomation.BA.BaParameterId.eProportionalConstant,
                TcHmi.BuildingAutomation.BA.BaParameterId.eIntegralConstant,
                TcHmi.BuildingAutomation.BA.BaParameterId.eDerivativeConstant,
                TcHmi.BuildingAutomation.BA.BaParameterId.eNeutralZone,
                TcHmi.BuildingAutomation.BA.BaParameterId.eOpMode,
                TcHmi.BuildingAutomation.BA.BaParameterId.eSynchronizedLoop
            ]],
            [TcHmi.BuildingAutomation.BA.BaParameterCategory.deviceManagement, [
                TcHmi.BuildingAutomation.BA.BaParameterId.eProjectInfo,
                TcHmi.BuildingAutomation.BA.BaParameterId.eOperatorInfo,
                TcHmi.BuildingAutomation.BA.BaParameterId.eTechnicalStaffInfo,
                TcHmi.BuildingAutomation.BA.BaParameterId.eEngineerInfo
            ]],
            [TcHmi.BuildingAutomation.BA.BaParameterCategory.networkManagement, []],
            [TcHmi.BuildingAutomation.BA.BaParameterCategory.hardware, [
                TcHmi.BuildingAutomation.BA.BaParameterId.eMappingMode,
                TcHmi.BuildingAutomation.BA.BaParameterId.eFeedbackMappingMode,
                TcHmi.BuildingAutomation.BA.BaParameterId.eRawFeedback,
                TcHmi.BuildingAutomation.BA.BaParameterId.eFeedbackPolarity,
                TcHmi.BuildingAutomation.BA.BaParameterId.eFeedbackValue,
                TcHmi.BuildingAutomation.BA.BaParameterId.eRawOverride,
                TcHmi.BuildingAutomation.BA.BaParameterId.eOverriddenPolarity,
                TcHmi.BuildingAutomation.BA.BaParameterId.eRawValue,
                TcHmi.BuildingAutomation.BA.BaParameterId.eRawState,
                TcHmi.BuildingAutomation.BA.BaParameterId.eTerminal,
                TcHmi.BuildingAutomation.BA.BaParameterId.eSensor,
                TcHmi.BuildingAutomation.BA.BaParameterId.eAddress,
                TcHmi.BuildingAutomation.BA.BaParameterId.eCommissioningState
            ]],
            [TcHmi.BuildingAutomation.BA.BaParameterCategory.additional, [
                TcHmi.BuildingAutomation.BA.BaParameterId.Additional
            ]],
            [TcHmi.BuildingAutomation.BA.BaParameterCategory.miscellaneous, [
                TcHmi.BuildingAutomation.BA.BaParameterId.Invalid,
                TcHmi.BuildingAutomation.BA.BaParameterId.eInstructionText,
                TcHmi.BuildingAutomation.BA.BaParameterId.eFaultValues,
                TcHmi.BuildingAutomation.BA.BaParameterId.eTrigger,
                TcHmi.BuildingAutomation.BA.BaParameterId.ePurpose,
                TcHmi.BuildingAutomation.BA.BaParameterId.eRebuildSegments,
                TcHmi.BuildingAutomation.BA.BaParameterId.eReferencedParam
            ]]
        ]);

        /*****************************************************************/
        /********************* ProjectNavigationList *********************/

        TcHmi.BuildingAutomation.Navigation.ProjectNavigationList.MaximumEventTypePulse = new Map([
            [TcHmi.BuildingAutomation.BA.Role.eGuest, TcHmi.BuildingAutomation.BA.EventType.eDisturb],
            [TcHmi.BuildingAutomation.BA.Role.eBasic, TcHmi.BuildingAutomation.BA.EventType.eDisturb],
            [TcHmi.BuildingAutomation.BA.Role.eAdvanced, TcHmi.BuildingAutomation.BA.EventType.eDisturb],
            [TcHmi.BuildingAutomation.BA.Role.eExpert, TcHmi.BuildingAutomation.BA.EventType.eDisturb],
            [TcHmi.BuildingAutomation.BA.Role.eInternal, TcHmi.BuildingAutomation.BA.EventType.eDisturb]
        ]);

        TcHmi.BuildingAutomation.Navigation.ProjectNavigationList.MaximumEventConditionDisplayed = new Map([
            [TcHmi.BuildingAutomation.BA.Role.eGuest, TcHmi.BuildingAutomation.BA.EventCondition.eTypeOther],
            [TcHmi.BuildingAutomation.BA.Role.eBasic, TcHmi.BuildingAutomation.BA.EventCondition.eTypeOther],
            [TcHmi.BuildingAutomation.BA.Role.eAdvanced, TcHmi.BuildingAutomation.BA.EventCondition.eEventIconDisplayed],
            [TcHmi.BuildingAutomation.BA.Role.eExpert, TcHmi.BuildingAutomation.BA.EventCondition.eEventIconDisplayed],
            [TcHmi.BuildingAutomation.BA.Role.eInternal, TcHmi.BuildingAutomation.BA.EventCondition.eEventIconDisplayed]
        ]);

        /*****************************************************************/
        /**************************** UserData ***************************/

        TcHmi.BuildingAutomation.Storage.UserData.LastContentStorageLocation = TcHmi.BuildingAutomation.Storage.Location.baSite;
        TcHmi.BuildingAutomation.Storage.UserData.LastThemeStorageLocation = TcHmi.BuildingAutomation.Storage.Location.baSite;
        TcHmi.BuildingAutomation.Storage.UserData.TrendSettingsStorageLocation = TcHmi.BuildingAutomation.Storage.Location.baSite;
        TcHmi.BuildingAutomation.Storage.UserData.TrendCollectionStorageLocation = TcHmi.BuildingAutomation.Storage.Location.baSite;
        TcHmi.BuildingAutomation.Storage.UserData.TrendCollectionSelectionStorageLocation = TcHmi.BuildingAutomation.Storage.Location.baSite;
    });
})(TcHmi);
