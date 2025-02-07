Attribute VB_Name = "rsnrpz"
'= R&S NRP-Z Power Meter Include File ========================================

'***************************************************************************
'= VISA constant and type definitions ======================================
'***************************************************************************

Global Const VI_ERROR = (-2147483647 - 1&) '/* 0x80000000 */

Global Const VI_WARN_NSUP_ID_QUERY = (&H3FFC0101)
Global Const VI_WARN_NSUP_RESET = (&H3FFC0102)
Global Const VI_WARN_NSUP_SELF_TEST = (&H3FFC0103)
Global Const VI_WARN_NSUP_ERROR_QUERY = (&H3FFC0104)
Global Const VI_WARN_NSUP_REV_QUERY = (&H3FFC0105)
Global Const VI_WARN_UNKNOWN_STATUS = (&H3FFF0085)

Global Const VI_ERROR_PARAMETER1 = (&H80000000 + &H3FFC0001)
Global Const VI_ERROR_PARAMETER2 = (&H80000000 + &H3FFC0002)
Global Const VI_ERROR_PARAMETER3 = (&H80000000 + &H3FFC0003)
Global Const VI_ERROR_PARAMETER4 = (&H80000000 + &H3FFC0004)
Global Const VI_ERROR_PARAMETER5 = (&H80000000 + &H3FFC0005)
Global Const VI_ERROR_PARAMETER6 = (&H80000000 + &H3FFC0006)
Global Const VI_ERROR_PARAMETER7 = (&H80000000 + &H3FFC0007)
Global Const VI_ERROR_PARAMETER8 = (&H80000000 + &H3FFC0008)
Global Const VI_ERROR_FAIL_ID_QUERY = (&H80000000 + &H3FFC0011)
Global Const VI_ERROR_INV_RESPONSE = (&H80000000 + &H3FFC0012)


'- Completion and Error Codes ----------------------------------------------

Global Const VI_SUCCESS = (0&)

'- Other VISA Definitions --------------------------------------------------

Global Const VI_TRUE = (1&)
Global Const VI_FALSE = (0&)

'***************************************************************************
'= Instrument Driver Specific Error/Warning Codes ==========================
'***************************************************************************
Global Const VI_ERROR_INSTR_INTERPRETING_RESPONSE = (&H80000000 + &H3FFC0803)
Global Const VI_ERROR_INSTR_PARAMETER9 = (&H80000000 + &H3FFC0809)
Global Const VI_ERROR_INSTR_PARAMETER10 = (&H80000000 + &H3FFC080A)
Global Const VI_ERROR_INSTR_PARAMETER11 = (&H80000000 + &H3FFC080B)
Global Const VI_ERROR_INSTR_PARAMETER12 = (&H80000000 + &H3FFC080C)
Global Const VI_ERROR_INSTR_PARAMETER13 = (&H80000000 + &H3FFC080D)
Global Const VI_ERROR_INSTR_PARAMETER14 = (&H80000000 + &H3FFC080E)
Global Const VI_ERROR_INSTR_PARAMETER15 = (&H80000000 + &H3FFC080F)

'***************************************************************************
'= Define Instrument Specific Error/Warning Codes Here =====================
'***************************************************************************
Global Const VI_WARNING_INSTR_OFFSET = (&H3FFC0900)
Global Const VI_ERROR_INSTR_OFFSET = (&HBFFC0900)

Global Const RSNRPZ_ERROR_INSTRUMENT_ERROR = (&HBFFC0900 + &HF0&)

'    ' Add instrument driver specific error codes here 
Global Const RSNRPZ_ERROR_INVALID_CONFIGURATION = (&HBFFC0900 + &HF1&)
Global Const RSNRPZ_ERROR_INSTRUMENT_OPTION = (&HBFFC0900 + &HF2&)
Global Const RSNRPZ_ERROR_INSTRUMENT_NSUP_MODEL = (&HBFFC0900 + &HF3&)
Global Const RSNRPZ_ERROR_SETTINGS_CONFLICT = (&HBFFC0900 + &HF4&)
Global Const RSNRPZ_ERROR_INTERPRETING_RESPONSE = (&HBFFC0900 + &HF5&)
Global Const RSNRPZ_ERROR_TABLE_NOT_FOUND = (&HBFFC0900 + &HF6&)
Global Const RSNRPZ_ERROR_COMMAND_NOT_FOUND = (&HBFFC0900 + &HF7&)
Global Const RSNRPZ_ERROR_COMMAND_NOT_SUPPORTED = (&HBFFC0900 + &HF8&)
Global Const RSNRPZ_ERROR_INVALID_VALUE = (&HBFFC0900 + &HF9&)
Global Const RSNRPZ_ERROR_INCORRECT_CHANNEL = (&HBFFC0900 + &HFA&)
Global Const RSNRPZ_ERROR_TIMEOUT = (&HBFFC0900 + &HFB&)
Global Const RSNRPZ_ERROR_MAX_REGISTER_EVENTS = (&HBFFC0900 + &HFC&)
Global Const RSNRPZ_ERROR_REGISTER_EVENT = (&HBFFC0900 + &HFD&)
Global Const RSNRPZ_ERROR_SENSOR_ASSIGNED_TO_CHANNEL = (&HBFFC0900 + &HFE&)
Global Const RSNRPZ_ERROR_SMALL_BUFFER = (&HBFFC0900 + &HFF&)
Global Const RSNRPZ_ERROR_MEAS_NOT_AVAILABLE = (&HBFFC0900 + &H100&)
Global Const RSNRPZ_ERROR_MAX_TIME_EXCEEDED = (&HBFFC0900 + &H101&)

Global Const RSNRPZ_WARNING_NO_CHANNEL = (VI_WARNING_INSTR_OFFSET + &H1&)

'***************************************************************************
'= Instrument specific defines =============================================
'***************************************************************************
Global Const RSNRPZ_ONCE = 3&
Global Const RSNRPZ_ZERO_LFR = 4&
Global Const RSNRPZ_ZERO_UFR = 5&

Global Const RSNRPZ_SLOPE_POSITIVE = 0&
Global Const RSNRPZ_SLOPE_NEGATIVE = 1&

Global Const RSNRPZ_AUTO_COUNT_TYPE_RESOLUTION = 0&
Global Const RSNRPZ_AUTO_COUNT_TYPE_NSR = 1&

Global Const RSNRPZ_TERMINAL_CONTROL_MOVING = 0&
Global Const RSNRPZ_TERMINAL_CONTROL_REPEAT = 1&

Global Const RSNRPZ_SENSOR_MODE_CONTAV = 0&
Global Const RSNRPZ_SENSOR_MODE_BUF_CONTAV = 1&
Global Const RSNRPZ_SENSOR_MODE_TIMESLOT = 2&
Global Const RSNRPZ_SENSOR_MODE_BURST = 3&
Global Const RSNRPZ_SENSOR_MODE_SCOPE = 4&
Global Const RSNRPZ_SENSOR_MODE_CCDF = 6&
Global Const RSNRPZ_SENSOR_MODE_PDF = 7&

Global Const RSNRPZ_TRIGGER_SOURCE_BUS = 0&
Global Const RSNRPZ_TRIGGER_SOURCE_EXTERNAL = 1&
Global Const RSNRPZ_TRIGGER_SOURCE_EXTERNAL1 = 1&
Global Const RSNRPZ_TRIGGER_SOURCE_HOLD = 2&
Global Const RSNRPZ_TRIGGER_SOURCE_IMMEDIATE = 3&
Global Const RSNRPZ_TRIGGER_SOURCE_INTERNAL = 4&
Global Const RSNRPZ_TRIGGER_SOURCE_EXT2 = 5&
Global Const RSNRPZ_TRIGGER_SOURCE_EXTERNAL2 = 5&

Global Const RSNRPZ_PORT_EXT = 1&
Global Const RSNRPZ_PORT_EXT1 = 1&
Global Const RSNRPZ_PORT_EXT2 = 5&

Global Const RSNRPZ_LEDMODE_USER = 1&
Global Const RSNRPZ_LEDMODE_SENSOR = 2&

Global Const RSNRPZ_SAMPLING_FREQUENCY1 = 1&
Global Const RSNRPZ_SAMPLING_FREQUENCY2 = 2&

Global Const RSNRPZ_AUX_NONE = 0&
Global Const RSNRPZ_AUX_MINMAX = 1&
Global Const RSNRPZ_AUX_RNDMAX = 2&

Global Const RSNRPZ_VAL_MAX_TIME_INFINITE = &HFFFFFFFF
Global Const RSNRPZ_VAL_MAX_TIME_IMMEDIATE = 0&

Global Const RSNRPZ_MEASUREMENT_SINGLE = 0&
Global Const RSNRPZ_MEASUREMENT_SINGLE_REL = 1&
Global Const RSNRPZ_MEASUREMENT_DIFF = 2&
Global Const RSNRPZ_MEASUREMENT_DIFF_REL = 3&
Global Const RSNRPZ_MEASUREMENT_SUM = 4&
Global Const RSNRPZ_MEASUREMENT_SUM_REL = 5&
Global Const RSNRPZ_MEASUREMENT_RATIO = 6&
Global Const RSNRPZ_MEASUREMENT_RATIO_REL = 7&
Global Const RSNRPZ_MEASUREMENT_SWR = 8&
Global Const RSNRPZ_MEASUREMENT_REFL = 9&
Global Const RSNRPZ_MEASUREMENT_RLOS = 10&

'- Register bits -
Global Const RSNRPZ_STATCLASS_D_CONN = (1&)
Global Const RSNRPZ_STATCLASS_D_ERR = (2&)
Global Const RSNRPZ_STATCLASS_O_CAL = (3&)
Global Const RSNRPZ_STATCLASS_O_MEAS = (4&)
Global Const RSNRPZ_STATCLASS_O_TRIGGER = (5&)
Global Const RSNRPZ_STATCLASS_O_SENSE = (6&)
Global Const RSNRPZ_STATCLASS_O_LOWER = (7&)
Global Const RSNRPZ_STATCLASS_O_UPPER = (8&)
Global Const RSNRPZ_STATCLASS_Q_POWER = (9&)
Global Const RSNRPZ_STATCLASS_Q_WINDOW = (10&)
Global Const RSNRPZ_STATCLASS_Q_CAL = (11&)
Global Const RSNRPZ_STATCLASS_Q_ZER = (12&)

Global Const RSNRPZ_DIRECTION_NONE = 0&
Global Const RSNRPZ_DIRECTION_PTR = 1&
Global Const RSNRPZ_DIRECTION_NTR = 2&
Global Const RSNRPZ_DIRECTION_BOTH = 3&

Global Const RSNRPZ_ALL_SENSORS = &HFFFFFFFE
Global Const RSNRPZ_SENSOR_01 = &H2&        '/* (1L << 1) */
Global Const RSNRPZ_SENSOR_02 = &H4&        '/* (1L << 2) */
Global Const RSNRPZ_SENSOR_03 = &H8&        '/* (1L << 3) */
Global Const RSNRPZ_SENSOR_04 = &H10&       '/* (1L << 4) */
Global Const RSNRPZ_SENSOR_05 = &H20&       '/* (1L << 5) */
Global Const RSNRPZ_SENSOR_06 = &H40&       '/* (1L << 6) */
Global Const RSNRPZ_SENSOR_07 = &H80&       '/* (1L << 7) */
Global Const RSNRPZ_SENSOR_08 = &H100&      '/* (1L << 8) */
Global Const RSNRPZ_SENSOR_09 = &H200&      '/* (1L << 9) */
Global Const RSNRPZ_SENSOR_10 = &H400&      '/* (1L << 10)*/
Global Const RSNRPZ_SENSOR_11 = &H800&      '/* (1L << 11)*/
Global Const RSNRPZ_SENSOR_12 = &H1000&     '/* (1L << 12)*/
Global Const RSNRPZ_SENSOR_13 = &H2000&     '/* (1L << 13)*/
Global Const RSNRPZ_SENSOR_14 = &H4000&     '/* (1L << 14)*/
Global Const RSNRPZ_SENSOR_15 = &H8000&     '/* (1L << 15)*/
Global Const RSNRPZ_SENSOR_16 = &H10000     '/* (1L << 16)*/
Global Const RSNRPZ_SENSOR_17 = &H20000     '/* (1L << 17)*/
Global Const RSNRPZ_SENSOR_18 = &H40000     '/* (1L << 18)*/
Global Const RSNRPZ_SENSOR_19 = &H80000     '/* (1L << 19)*/
Global Const RSNRPZ_SENSOR_20 = &H100000    '/* (1L << 20)*/
Global Const RSNRPZ_SENSOR_21 = &H200000    '/* (1L << 21)*/
Global Const RSNRPZ_SENSOR_22 = &H400000    '/* (1L << 22)*/
Global Const RSNRPZ_SENSOR_23 = &H800000    '/* (1L << 23)*/
Global Const RSNRPZ_SENSOR_24 = &H1000000   '/* (1L << 24)*/
Global Const RSNRPZ_SENSOR_25 = &H2000000   '/* (1L << 25)*/
Global Const RSNRPZ_SENSOR_26 = &H4000000   '/* (1L << 26)*/
Global Const RSNRPZ_SENSOR_27 = &H8000000   '/* (1L << 27)*/
Global Const RSNRPZ_SENSOR_28 = &H10000000  '/* (1L << 28)*/
Global Const RSNRPZ_SENSOR_29 = &H20000000  '/* (1L << 29)*/
Global Const RSNRPZ_SENSOR_30 = &H40000000  '/* (1L << 30)*/
Global Const RSNRPZ_SENSOR_31 = &H80000000  '/* (1L << 31)*/

Global Const RSNRPZ_SCOPE_MEAS_ALG_HIST = 0&
Global Const RSNRPZ_SCOPE_MEAS_ALG_INT = 1&
Global Const RSNRPZ_SCOPE_MEAS_ALG_PEAK = 3&

Global Const RSNRPZ_SPACING_LINEAR = 0&
Global Const RSNRPZ_SPACING_LOG = 1&

Global Const RSNRPZ_Z5_PORT_A = 0&
Global Const RSNRPZ_Z5_PORT_B = 1&
Global Const RSNRPZ_Z5_PORT_C = 2&
Global Const RSNRPZ_Z5_PORT_D = 3&

'***************************************************************************
'= GLOBAL USER-CALLABLE FUNCTION DECLARATIONS (Exportable Functions) =======
'***************************************************************************
Declare Function rsnrpz_setTimeout Lib "rsnrpz_32.dll" ( ByVal ulNewTimo As Long) As Long
Declare Function rsnrpz_getTimeout Lib "rsnrpz_32.dll" ( pulNewTimo As Long) As Long
Declare Function rsnrpz_init Lib "rsnrpz_32.dll" ( ByVal resourceName As String, ByVal IDQuery As Integer, ByVal resetDevice As Integer, instrumentHandle As Long) As Long
Declare Function rsnrpz_init_long_distance Lib "rsnrpz_32.dll" ( ByVal IDQuery As Integer, ByVal resetDevice As Integer, ByVal resourceName As String, instrumentHandle As Long) As Long
Declare Function rsnrpz_AddSensor Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, ByVal resourceName As String, ByVal IDQuery As Integer, ByVal resetDevice As Integer) As Long
Declare Function rsnrpz_chans_abort Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long) As Long
Declare Function rsnrpz_chans_getCount Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, count As Long) As Long
Declare Function rsnrpz_chans_initiate Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long) As Long
Declare Function rsnrpz_chans_zero Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long) As Long
Declare Function rsnrpz_chan_zeroAdvanced Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, ByVal zeroing As Long) As Long
Declare Function rsnrpz_chans_isZeroingComplete Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, zeroingCompleted As Integer) As Long
Declare Function rsnrpz_chans_isMeasurementComplete Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, measurementCompleted As Integer) As Long
Declare Function rsnrpz_chan_mode Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, ByVal measurementMode As Long) As Long
Declare Function rsnrpz_timing_configureExclude Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, ByVal excludeStart As Double, ByVal excludeStop As Double) As Long
Declare Function rsnrpz_timing_setTimingExcludeStart Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, ByVal excludeStart As Double) As Long
Declare Function rsnrpz_timing_getTimingExcludeStart Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, excludeStart As Double) As Long
Declare Function rsnrpz_timing_setTimingExcludeStop Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, ByVal excludeStop As Double) As Long
Declare Function rsnrpz_timing_getTimingExcludeStop Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, excludeStop As Double) As Long
Declare Function rsnrpz_bandwidth_setBw Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, ByVal bandwidth As Long) As Long
Declare Function rsnrpz_bandwidth_getBw Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, bandwidth As Long) As Long
Declare Function rsnrpz_bandwidth_getBwList Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, ByVal bufferSize As Long, ByVal bandwidthList As String) As Long
Declare Function rsnrpz_avg_configureAvgAuto Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, ByVal resolution As Long) As Long
Declare Function rsnrpz_avg_configureAvgNSRatio Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, ByVal maximumNoiseRatio As Double, ByVal upperTimeLimit As Double) As Long
Declare Function rsnrpz_avg_configureAvgManual Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, ByVal count As Long) As Long
Declare Function rsnrpz_avg_setAutoEnabled Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, ByVal autoEnabled As Integer) As Long
Declare Function rsnrpz_avg_getAutoEnabled Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, autoEnabled As Integer) As Long
Declare Function rsnrpz_avg_setAutoMaxMeasuringTime Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, ByVal upperTimeLimit As Double) As Long
Declare Function rsnrpz_avg_getAutoMaxMeasuringTime Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, upperTimeLimit As Double) As Long
Declare Function rsnrpz_avg_setAutoNoiseSignalRatio Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, ByVal maximumNoiseRatio As Double) As Long
Declare Function rsnrpz_avg_getAutoNoiseSignalRatio Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, maximumNoiseRatio As Double) As Long
Declare Function rsnrpz_avg_setAutoResolution Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, ByVal resolution As Long) As Long
Declare Function rsnrpz_avg_getAutoResolution Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, resolution As Long) As Long
Declare Function rsnrpz_avg_setAutoType Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, ByVal method As Long) As Long
Declare Function rsnrpz_avg_getAutoType Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, method As Long) As Long
Declare Function rsnrpz_avg_setCount Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, ByVal count As Long) As Long
Declare Function rsnrpz_avg_getCount Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, count As Long) As Long
Declare Function rsnrpz_avg_setEnabled Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, ByVal averaging As Integer) As Long
Declare Function rsnrpz_avg_getEnabled Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, averaging As Integer) As Long
Declare Function rsnrpz_avg_setSlot Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, ByVal timeslot As Long) As Long
Declare Function rsnrpz_avg_getSlot Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, timeslot As Long) As Long
Declare Function rsnrpz_avg_setTerminalControl Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, ByVal terminalControl As Long) As Long
Declare Function rsnrpz_avg_getTerminalControl Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, terminalControl As Long) As Long
Declare Function rsnrpz_avg_reset Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long) As Long
Declare Function rsnrpz_range_setAutoEnabled Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, ByVal autoRange As Integer) As Long
Declare Function rsnrpz_range_getAutoEnabled Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, autoRange As Integer) As Long
Declare Function rsnrpz_range_setCrossoverLevel Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, ByVal crossoverLevel As Double) As Long
Declare Function rsnrpz_range_getCrossoverLevel Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, crossoverLevel As Double) As Long
Declare Function rsnrpz_range_setRange Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, ByVal range As Long) As Long
Declare Function rsnrpz_range_getRange Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, range As Long) As Long
Declare Function rsnrpz_corr_configureCorrections Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, ByVal offsetState As Integer, ByVal offset As Double, ByVal reserved1 As Integer, ByVal reserved2 As String, ByVal sParameterEnable As Integer) As Long
Declare Function rsnrpz_chan_setCorrectionFrequency Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, ByVal frequency As Double) As Long
Declare Function rsnrpz_chan_getCorrectionFrequency Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, frequency As Double) As Long
Declare Function rsnrpz_chan_setCorrectionFrequencyStep Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, ByVal frequencyStep As Double) As Long
Declare Function rsnrpz_chan_getCorrectionFrequencyStep Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, frequencyStep As Double) As Long
Declare Function rsnrpz_chan_setCorrectionFrequencySpacing Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, ByVal frequencySpacing As Long) As Long
Declare Function rsnrpz_chan_getCorrectionFrequencySpacing Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, frequencySpacing As Long) As Long
Declare Function rsnrpz_corr_setOffset Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, ByVal offset As Double) As Long
Declare Function rsnrpz_corr_getOffset Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, offset As Double) As Long
Declare Function rsnrpz_corr_setOffsetEnabled Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, ByVal offsetState As Integer) As Long
Declare Function rsnrpz_corr_getOffsetEnabled Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, offsetState As Integer) As Long
Declare Function rsnrpz_corr_setSParamDeviceEnabled Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, ByVal sParameterEnable As Integer) As Long
Declare Function rsnrpz_corr_getSParamDeviceEnabled Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, sParameterCorrection As Integer) As Long
Declare Function rsnrpz_corr_setSParamDevice Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, ByVal sParameter As Long) As Long
Declare Function rsnrpz_corr_getSParamDevice Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, sParameter As Long) As Long
Declare Function rsnrpz_corr_getSParamDevList Lib "rsnrpz_32.dll" ( ByVal vi As Long, ByVal channel As Long, ByVal iSpdListSize As Long, ByVal spdList As String) As Long
Declare Function rsnrpz_chan_configureSourceGammaCorr Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, ByVal sourceGammaCorrection As Integer, ByVal magnitude As Double, ByVal phase As Double) As Long
Declare Function rsnrpz_chan_setSourceGammaMagnitude Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, ByVal magnitude As Double) As Long
Declare Function rsnrpz_chan_getSourceGammaMagnitude Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, magnitude As Double) As Long
Declare Function rsnrpz_chan_setSourceGammaPhase Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, ByVal phase As Double) As Long
Declare Function rsnrpz_chan_getSourceGammaPhase Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, phase As Double) As Long
Declare Function rsnrpz_chan_setSourceGammaCorrEnabled Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, ByVal sourceGammaCorrection As Integer) As Long
Declare Function rsnrpz_chan_getSourceGammaCorrEnabled Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, sourceGammaCorrection As Integer) As Long
Declare Function rsnrpz_chan_configureReflectGammaCorr Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, ByVal magnitude As Double, ByVal phase As Double) As Long
Declare Function rsnrpz_chan_setReflectionGammaMagn Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, ByVal magnitude As Double) As Long
Declare Function rsnrpz_chan_getReflectionGammaMagn Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, magnitude As Double) As Long
Declare Function rsnrpz_chan_setReflectionGammaPhase Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, ByVal phase As Double) As Long
Declare Function rsnrpz_chan_getReflectionGammaPhase Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, phase As Double) As Long
Declare Function rsnrpz_chan_setReflectionGammaUncertainty Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, ByVal uncertainty As Double) As Long
Declare Function rsnrpz_chan_getReflectionGammaUncertainty Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, uncertainty As Double) As Long
Declare Function rsnrpz_corr_configureDutyCycle Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, ByVal dutyCycleState As Integer, ByVal dutyCycle As Double) As Long
Declare Function rsnrpz_corr_setDutyCycle Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, ByVal dutyCycle As Double) As Long
Declare Function rsnrpz_corr_getDutyCycle Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, dutyCycle As Double) As Long
Declare Function rsnrpz_corr_setDutyCycleEnabled Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, ByVal dutyCycleState As Integer) As Long
Declare Function rsnrpz_corr_getDutyCycleEnabled Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, dutyCycleState As Integer) As Long
Declare Function rsnrpz_chan_setContAvAperture Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, ByVal contAvAperture As Double) As Long
Declare Function rsnrpz_chan_getContAvAperture Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, contAvAperture As Double) As Long
Declare Function rsnrpz_chan_setContAvSmoothingEnabled Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, ByVal contAvSmoothing As Integer) As Long
Declare Function rsnrpz_chan_getContAvSmoothingEnabled Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, contAvSmoothing As Integer) As Long
Declare Function rsnrpz_chan_setContAvBufferedEnabled Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, ByVal contAvBufferedMode As Integer) As Long
Declare Function rsnrpz_chan_getContAvBufferedEnabled Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, contAvBufferedMode As Integer) As Long
Declare Function rsnrpz_chan_setContAvBufferSize Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, ByVal bufferSize As Long) As Long
Declare Function rsnrpz_chan_getContAvBufferSize Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, bufferSize As Long) As Long
Declare Function rsnrpz_chan_getContAvBufferCount Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, bufferCount As Long) As Long
Declare Function rsnrpz_chan_getContAvBufferInfo Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, ByVal infoType As String, ByVal arraySize As Long, ByVal info As String) As Long
Declare Function rsnrpz_chan_setBurstDropoutTolerance Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, ByVal dropoutTolerance As Double) As Long
Declare Function rsnrpz_chan_getBurstDropoutTolerance Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, dropoutTolerance As Double) As Long
Declare Function rsnrpz_chan_setBurstChopperEnabled Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, ByVal burstAvChopper As Integer) As Long
Declare Function rsnrpz_chan_getBurstChopperEnabled Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, burstAvChopper As Integer) As Long
Declare Function rsnrpz_stat_confTimegate Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, ByVal offset As Double, ByVal time As Double, ByVal midambleOffset As Double, ByVal midambleLength As Double) As Long
Declare Function rsnrpz_stat_confScale Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, ByVal referenceLevel As Double, ByVal range As Double, ByVal points As Long) As Long
Declare Function rsnrpz_stat_setOffsetTime Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, ByVal offset As Double) As Long
Declare Function rsnrpz_stat_getOffsetTime Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, offset As Double) As Long
Declare Function rsnrpz_stat_setTime Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, ByVal time As Double) As Long
Declare Function rsnrpz_stat_getTime Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, time As Double) As Long
Declare Function rsnrpz_stat_setMidOffset Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, ByVal offset As Double) As Long
Declare Function rsnrpz_stat_getMidOffset Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, offset As Double) As Long
Declare Function rsnrpz_stat_setMidLength Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, ByVal length As Double) As Long
Declare Function rsnrpz_stat_getMidLength Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, length As Double) As Long
Declare Function rsnrpz_stat_setScaleRefLevel Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, ByVal referenceLevel As Double) As Long
Declare Function rsnrpz_stat_getScaleRefLevel Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, referenceLevel As Double) As Long
Declare Function rsnrpz_stat_setScaleRange Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, ByVal range As Double) As Long
Declare Function rsnrpz_stat_getScaleRange Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, range As Double) As Long
Declare Function rsnrpz_stat_setScalePoints Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, ByVal points As Long) As Long
Declare Function rsnrpz_stat_getScalePoints Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, points As Long) As Long
Declare Function rsnrpz_stat_getScaleWidth Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, width As Double) As Long
Declare Function rsnrpz_tslot_configureTimeSlot Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, ByVal timeSlotCount As Long, ByVal width As Double) As Long
Declare Function rsnrpz_tslot_setTimeSlotCount Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, ByVal timeSlotCount As Long) As Long
Declare Function rsnrpz_tslot_getTimeSlotCount Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, timeSlotCount As Long) As Long
Declare Function rsnrpz_tslot_setTimeSlotWidth Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, ByVal width As Double) As Long
Declare Function rsnrpz_tslot_getTimeSlotWidth Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, width As Double) As Long
Declare Function rsnrpz_tslot_setTimeSlotMidOffset Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, ByVal offset As Double) As Long
Declare Function rsnrpz_tslot_getTimeSlotMidOffset Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, offset As Double) As Long
Declare Function rsnrpz_tslot_setTimeSlotMidLength Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, ByVal length As Double) As Long
Declare Function rsnrpz_tslot_getTimeSlotMidLength Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, length As Double) As Long
Declare Function rsnrpz_tslot_setTimeSlotChopperEnabled Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, ByVal timeSlotChopper As Integer) As Long
Declare Function rsnrpz_tslot_getTimeSlotChopperEnabled Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, timeSlotChopper As Integer) As Long
Declare Function rsnrpz_scope_configureScope Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, ByVal scopePoints As Long, ByVal scopeTime As Double, ByVal offsetTime As Double, ByVal realtime As Integer) As Long
Declare Function rsnrpz_scope_fastZero Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long) As Long
Declare Function rsnrpz_scope_setAverageEnabled Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, ByVal scopeAveraging As Integer) As Long
Declare Function rsnrpz_scope_getAverageEnabled Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, scopeAveraging As Integer) As Long
Declare Function rsnrpz_scope_setAverageCount Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, ByVal count As Long) As Long
Declare Function rsnrpz_scope_getAverageCount Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, count As Long) As Long
Declare Function rsnrpz_scope_setAverageTerminalControl Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, ByVal terminalControl As Long) As Long
Declare Function rsnrpz_scope_getAverageTerminalControl Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, terminalControl As Long) As Long
Declare Function rsnrpz_scope_setOffsetTime Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, ByVal offsetTime As Double) As Long
Declare Function rsnrpz_scope_getOffsetTime Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, offsetTime As Double) As Long
Declare Function rsnrpz_scope_setPoints Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, ByVal scopePoints As Long) As Long
Declare Function rsnrpz_scope_getPoints Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, scopePoints As Long) As Long
Declare Function rsnrpz_scope_setRealtimeEnabled Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, ByVal realtime As Integer) As Long
Declare Function rsnrpz_scope_getRealtimeEnabled Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, realtime As Integer) As Long
Declare Function rsnrpz_scope_setTime Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, ByVal scopeTime As Double) As Long
Declare Function rsnrpz_scope_getTime Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, scopeTime As Double) As Long
Declare Function rsnrpz_scope_setAutoEnabled Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, ByVal autoEnabled As Integer) As Long
Declare Function rsnrpz_scope_getAutoEnabled Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, autoEnabled As Integer) As Long
Declare Function rsnrpz_scope_setAutoMaxMeasuringTime Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, ByVal upperTimeLimit As Double) As Long
Declare Function rsnrpz_scope_getAutoMaxMeasuringTime Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, upperTimeLimit As Double) As Long
Declare Function rsnrpz_scope_setAutoNoiseSignalRatio Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, ByVal maximumNoiseRatio As Double) As Long
Declare Function rsnrpz_scope_getAutoNoiseSignalRatio Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, maximumNoiseRatio As Double) As Long
Declare Function rsnrpz_scope_setAutoResolution Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, ByVal resolution As Long) As Long
Declare Function rsnrpz_scope_getAutoResolution Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, resolution As Long) As Long
Declare Function rsnrpz_scope_setAutoType Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, ByVal method As Long) As Long
Declare Function rsnrpz_scope_getAutoType Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, method As Long) As Long
Declare Function rsnrpz_scope_setEquivalentSampling Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, ByVal scopeEquivalentSampling As Integer) As Long
Declare Function rsnrpz_scope_getEquivalentSampling Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, scopeEquivalentSampling As Integer) As Long
Declare Function rsnrpz_scope_meas_setMeasEnabled Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, ByVal traceMeasurements As Integer) As Long
Declare Function rsnrpz_scope_meas_getMeasEnabled Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, traceMeasurements As Integer) As Long
Declare Function rsnrpz_scope_meas_setMeasAlgorithm Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, ByVal algorithm As Long) As Long
Declare Function rsnrpz_scope_meas_getMeasAlgorithm Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, algorithm As Long) As Long
Declare Function rsnrpz_scope_meas_setLevelThresholds Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, ByVal durationRef As Double, ByVal transitionLowRef As Double, ByVal transitionHighRef As Double) As Long
Declare Function rsnrpz_scope_meas_getLevelThresholds Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, durationRef As Double, transitionLowRef As Double, transitionHighRef As Double) As Long
Declare Function rsnrpz_scope_meas_setTime Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, ByVal measTime As Double) As Long
Declare Function rsnrpz_scope_meas_getTime Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, measTime As Double) As Long
Declare Function rsnrpz_scope_meas_setOffsetTime Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, ByVal offsetTime As Double) As Long
Declare Function rsnrpz_scope_meas_getOffsetTime Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, offsetTime As Double) As Long
Declare Function rsnrpz_scope_meas_getPulseTimes Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, dutyCycle As Double, pulseDuration As Double, pulsePeriod As Double) As Long
Declare Function rsnrpz_scope_meas_getPulseTransition Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, ByVal slope As Long, duration As Double, occurence As Double, overshoot As Double) As Long
Declare Function rsnrpz_scope_meas_getPulsePower Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, average As Double, minPeak As Double, maxPeak As Double) As Long
Declare Function rsnrpz_scope_meas_getPulseLevels Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, topLevel As Double, baseLevel As Double) As Long
Declare Function rsnrpz_scope_meas_getPulseReferenceLevels Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, lowRefLevel As Double, highRefLevel As Double, durationRefLevel As Double) As Long
Declare Function rsnrpz_scope_meas_setEquivalentSampling Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, ByVal scopeMeasEquivSampling As Integer) As Long
Declare Function rsnrpz_scope_meas_getEquivalentSampling Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, scopeMeasEquivSampling As Integer) As Long
Declare Function rsnrpz_scope_meas_getSamplePeriod Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, samplePeriod As Double) As Long
Declare Function rsnrpz_trigger_configureInternal Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, ByVal triggerLevel As Double, ByVal triggerSlope As Long) As Long
Declare Function rsnrpz_trigger_configureExternal Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, ByVal triggerDelay As Double) As Long
Declare Function rsnrpz_trigger_immediate Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long) As Long
Declare Function rsnrpz_trigger_setAutoDelayEnabled Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, ByVal autoDelay As Integer) As Long
Declare Function rsnrpz_trigger_getAutoDelayEnabled Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, autoDelay As Integer) As Long
Declare Function rsnrpz_trigger_setAutoTriggerEnabled Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, ByVal autoTrigger As Integer) As Long
Declare Function rsnrpz_trigger_getAutoTriggerEnabled Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, autoTrigger As Integer) As Long
Declare Function rsnrpz_trigger_setCount Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, ByVal triggerCount As Long) As Long
Declare Function rsnrpz_trigger_getCount Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, triggerCount As Long) As Long
Declare Function rsnrpz_trigger_setDelay Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, ByVal triggerDelay As Double) As Long
Declare Function rsnrpz_trigger_getDelay Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, triggerDelay As Double) As Long
Declare Function rsnrpz_trigger_setHoldoff Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, ByVal triggerHoldoff As Double) As Long
Declare Function rsnrpz_trigger_getHoldoff Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, triggerHoldoff As Double) As Long
Declare Function rsnrpz_trigger_setHysteresis Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, ByVal triggerHysteresis As Double) As Long
Declare Function rsnrpz_trigger_getHysteresis Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, triggerHysteresis As Double) As Long
Declare Function rsnrpz_trigger_setLevel Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, ByVal triggerLevel As Double) As Long
Declare Function rsnrpz_trigger_getLevel Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, triggerLevel As Double) As Long
Declare Function rsnrpz_trigger_setSlope Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, ByVal triggerSlope As Long) As Long
Declare Function rsnrpz_trigger_getSlope Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, triggerSlope As Long) As Long
Declare Function rsnrpz_trigger_setSource Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, ByVal triggerSource As Long) As Long
Declare Function rsnrpz_trigger_getSource Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, triggerSource As Long) As Long
Declare Function rsnrpz_trigger_setDropoutTime Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, ByVal dropoutTime As Double) As Long
Declare Function rsnrpz_trigger_getDropoutTime Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, dropoutTime As Double) As Long
Declare Function rsnrpz_trigger_setMasterState Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, ByVal x_state As Integer) As Long
Declare Function rsnrpz_trigger_getMasterState Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, x_state As Integer) As Long
Declare Function rsnrpz_trigger_setSyncState Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, ByVal x_state As Integer) As Long
Declare Function rsnrpz_trigger_getSyncState Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, x_state As Integer) As Long
Declare Function rsnrpz_chan_info Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, ByVal infoType As String, ByVal arraySize As Long, ByVal info As String) As Long
Declare Function rsnrpz_chan_infoHeader Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, ByVal parameterNumber As Long, ByVal arraySize As Long, ByVal header As String) As Long
Declare Function rsnrpz_chan_infosCount Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, count As Long) As Long
Declare Function rsnrpz_system_setStatusUpdateTime Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, ByVal statusUpdateTime As Double) As Long
Declare Function rsnrpz_system_getStatusUpdateTime Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, statusUpdateTime As Double) As Long
Declare Function rsnrpz_system_setResultUpdateTime Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, ByVal resultUpdateTime As Double) As Long
Declare Function rsnrpz_system_getResultUpdateTime Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, resultUpdateTime As Double) As Long
Declare Function rsnrpz_calib_test Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, calibTest As Double) As Long
Declare Function rsnrpz_calib_getTestDeviation Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, testDeviation As Double) As Long
Declare Function rsnrpz_calib_getTestReference Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, testReference As Double) As Long
Declare Function rsnrpz_chan_abort Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long) As Long
Declare Function rsnrpz_chan_initiate Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long) As Long
Declare Function rsnrpz_chan_setInitContinuousEnabled Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, ByVal continuousInitiate As Integer) As Long
Declare Function rsnrpz_chan_getInitContinuousEnabled Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, continuousInitiate As Integer) As Long
Declare Function rsnrpz_chan_reset Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long) As Long
Declare Function rsnrpz_chan_setSamplingFrequency Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, ByVal samplingFrequency As Long) As Long
Declare Function rsnrpz_chan_getSamplingFrequency Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, samplingFrequency As Long) As Long
Declare Function rsnrpz_chan_zero Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long) As Long
Declare Function rsnrpz_chan_isZeroComplete Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, zeroingComplete As Integer) As Long
Declare Function rsnrpz_chan_isMeasurementComplete Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, measurementComplete As Integer) As Long
Declare Function rsnrpz_chan_selfTest Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, ByVal result As String) As Long
Declare Function rsnrpz_chan_setAuxiliary Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, ByVal auxiliaryValue As Long) As Long
Declare Function rsnrpz_chan_getAuxiliary Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, auxiliaryValue As Long) As Long
Declare Function rsnrpz_meass_readMeasurement Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, ByVal timeout_ms As Long, measurement As Double) As Long
Declare Function rsnrpz_meass_fetchMeasurement Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, measurement As Double) As Long
Declare Function rsnrpz_meass_readBufferMeasurement Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, ByVal maximumTime_ms As Long, ByVal bufferSize As Long, measurementArray As Double, readCount As Long) As Long
Declare Function rsnrpz_meass_fetchBufferMeasurement Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, ByVal arraySize As Long, measurementArray As Double, readCount As Long) As Long
Declare Function rsnrpz_meass_sendSoftwareTrigger Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long) As Long
Declare Function rsnrpz_meass_readMeasurementAux Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, ByVal timeout_ms As Long, measurement As Double, aux1 As Double, aux2 As Double) As Long
Declare Function rsnrpz_meass_fetchMeasurementAux Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, ByVal timeout_ms As Long, measurement As Double, aux1 As Double, aux2 As Double) As Long
Declare Function rsnrpz_meass_readBufferMeasurementAux Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, ByVal maximumTime_ms As Long, ByVal bufferSize As Long, measurementArray As Double, aux1Array As Double, aux2Array As Double, readCount As Long) As Long
Declare Function rsnrpz_meass_fetchBufferMeasurementAux Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long, ByVal maximumTime_ms As Long, ByVal bufferSize As Long, measurementArray As Double, aux1Array As Double, aux2Array As Double, readCount As Long) As Long
Declare Function rsnrpz_status_preset Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long) As Long
Declare Function rsnrpz_status_checkCondition Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal statusClass As Long, ByVal mask As Long, x_state As Integer) As Long
Declare Function rsnrpz_status_catchEvent Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal statusClass As Long, ByVal mask As Long, ByVal direction As Long) As Long
Declare Function rsnrpz_status_checkEvent Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal statusClass As Long, ByVal mask As Long, ByVal resetMask As Long, events As Integer) As Long
Declare Function rsnrpz_status_enableEventNotification Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal statusClass As Long, ByVal mask As Long) As Long
Declare Function rsnrpz_status_disableEventNotification Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal statusClass As Long, ByVal mask As Long) As Long
Declare Function rsnrpz_status_registerWindowMessage Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, windowHandle As Long, ByVal messageID As Long) As Long
Declare Function rsnrpz_errorCheckState Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal stateChecking As Integer) As Long
Declare Function rsnrpz_reset Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long) As Long
Declare Function rsnrpz_self_test Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, selfTestResult As Integer, ByVal selfTestMessage As String) As Long
Declare Function rsnrpz_error_query Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, errorCode As Long, ByVal errorMessage As String) As Long
Declare Function rsnrpz_error_message Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal statusCode As Long, ByVal message As String) As Long
Declare Function rsnrpz_revision_query Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal instrumentDriverRevision As String, ByVal firmwareRevision As String) As Long
Declare Function rsnrpz_CloseSensor Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long, ByVal channel As Long) As Long
Declare Function rsnrpz_close Lib "rsnrpz_32.dll" ( ByVal instrumentHandle As Long) As Long

Declare Function rsnrpz_GetSensorCount Lib "rsnrpz_32.dll" ( ByVal iDummyHandle As Long, piCount As Long) As Long
Declare Function rsnrpz_GetSensorInfo Lib "rsnrpz_32.dll" ( ByVal iDummyHandle As Long, ByVal iChannel As Long, ByVal pszSensorName As String, ByVal pszSensorType As String, ByVal pszSensorSerial As String) As Long

'***************************************************************************
' Support of R&S USB Hub NRP-Z5                                             
'***************************************************************************
Declare Function rsnrpz_GetDeviceStatusZ5 Lib "rsnrpz_32.dll" ( ByVal iDummyHandle As Long, piAvail As Long) As Long
Declare Function rsnrpz_GetDeviceInfoZ5 Lib "rsnrpz_32.dll" ( ByVal iDummyHandle As Long, ByVal iPortIdx As Long, ByVal pszSensorName As String, ByVal pszSensorType As String, ByVal pszSensorSerial As String, pbConnected As Integer) As Long
Declare Function rsnrpz_initZ5 Lib "rsnrpz_32.dll" ( ByVal cPort As Long, ByVal IDQuery As Integer, ByVal resetDevice As Integer, pInstrSession As Long) As Long
Declare Function rsnrpz_status_driverOpenState Lib "rsnrpz_32.dll" ( driverState As Integer) As Long
Declare Function rsnrpz_service_getDetectorTemperature Lib "rsnrpz_32.dll" ( ByVal vi As Long, ByVal channel As Long, pTemperature As Double) As Long
Declare Function rsnrpz_fw_version_check Lib "rsnrpz_32.dll" ( ByVal vi As Long, ByVal iBufSize As Long, ByVal firmwareCurrent As String, ByVal firmwareRequiredMinimum As String, pbFirmwareOkay As Integer) As Long
Declare Function rsnrpz_setSensorName Lib "rsnrpz_32.dll" ( ByVal instrHdl As Long, ByVal chan As Long, ByVal pName As String)  As Long
Declare Function rsnrpz_getSensorName Lib "rsnrpz_32.dll" ( ByVal instrHdl As Long, ByVal chan As Long, ByVal pName As String, ByVal maxLen As Long) As Long
Declare Function rsnrpz_setLedMode Lib "rsnrpz_32.dll" ( ByVal instrHdl As Long, ByVal chan As Long, ByVal ledMode As Long) As Long
Declare Function rsnrpz_getLedMode Lib "rsnrpz_32.dll" ( ByVal instrHdl As Long, ByVal chan As Long, pLedMode As Long) As Long
Declare Function rsnrpz_setLedColor Lib "rsnrpz_32.dll" ( ByVal instrHdl As Long, ByVal chan As Long, ByVal x_color As Long) As Long
Declare Function rsnrpz_getLedColor Lib "rsnrpz_32.dll" ( ByVal instrHdl As Long, ByVal chan As Long, pColor As Long) As Long
Declare Function rsnrpz_trigger_setMasterPort Lib "rsnrpz_32.dll" ( ByVal instrHdl As Long, ByVal chan As Long, ByVal x_port As Long) As Long
Declare Function rsnrpz_trigger_getMasterPort Lib "rsnrpz_32.dll" ( ByVal instrHdl As Long, ByVal chan As Long, x_port As Long) As Long
Declare Function rsnrpz_trigger_setSyncPort Lib "rsnrpz_32.dll" ( ByVal instrHdl As Long, ByVal chan As Long, ByVal x_port As Long) As Long
Declare Function rsnrpz_trigger_getSyncPort Lib "rsnrpz_32.dll" ( ByVal instrHdl As Long, ByVal chan As Long, x_port As Long) As Long
Declare Function rsnrpz_getUsageMap Lib "rsnrpz_32.dll" ( ByVal iDummyHandle As Long, ByVal cpMap As String, ByVal maxLen As Long, pRetLen As Long) As Long
