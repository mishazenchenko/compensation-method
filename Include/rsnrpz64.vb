Option Strict Off
Option Explicit On

Imports System
Imports System.Runtime.InteropServices


Public Class rsnrpz
    Inherits Object
    Implements System.IDisposable
    
    Private _handle As System.Runtime.InteropServices.HandleRef
    
    Private _disposed As Boolean = true
    
    '''<summary>
    '''This function creates an IVI instrument driver session, typically using the C session instrument handle.
    '''</summary>
    '''<param name="Instrument_Handle">
    '''The instrument handle that is used to create an IVI instrument driver session.
    '''</param>
    Public Sub New(ByVal Instrument_Handle As System.IntPtr)
        MyBase.New
        Me._handle = New System.Runtime.InteropServices.HandleRef(Me, Instrument_Handle)
        Me._disposed = false
    End Sub
    
    '''<summary>
    '''This function performs the following initialization actions and assigned specified sesnor to channel 1:
    '''
    '''- Opens a session to the specified device using the interface and address specified in the Resource Name control.
    '''
    '''- Performs an identification query on the sensor.
    '''
    '''- Resets the instrument to a known state.
    '''
    '''- Sends initialization commands to the sensor
    '''
    '''- Returns an Instrument Handle which is used to differentiate between different sessions of this instrument driver.
    '''</summary>
    '''<param name="Resource_Name">
    '''This control specifies the interface and address of the device that is to be initialized (Instrument Descriptor). The exact grammar to be used in this control is shown in the note below. 
    '''
    '''Default Value:  "USB::0x0aad::0x000b::100000"
    '''
    '''Notes:
    '''
    '''(1) Based on the Instrument Descriptor, this operation establishes a communication session with a device.  The grammar for the Instrument Descriptor is shown below.  Optional parameters are shown in square brackets ([]).
    '''
    '''Interface   Grammar
    '''------------------------------------------------------
    '''USB         USB::vendor Id::product Id::serial number
    '''
    '''where:
    '''  vendor Id is 0aad for all Rohde&amp;Schwarz instruments.
    '''
    '''  product Id depends on your sensor model:
    '''
    '''                       NRP-Z21  : 0x0003
    '''                       NRP-FU   : 0x0004
    '''                       FSH-Z1   : 0x000b
    '''                       NRP-Z11  : 0x000c
    '''                       NRP-Z22  : 0x0013
    '''                       NRP-Z23  : 0x0014
    '''                       NRP-Z24  : 0x0015
    '''                       NRP-Z51  : 0x0016
    '''                       NRP-Z52  : 0x0017
    '''                       NRP-Z55  : 0x0018
    '''                       NRP-Z56  : 0x0019
    '''                       FSH-Z18  : 0x001a
    '''                       NRP-Z91  : 0x0021
    '''                       NRP-Z81  : 0x0023
    '''                       NRP-Z31  : 0x002c
    '''                       NRP-Z37  : 0x002d
    '''                       NRP-Z96  : 0x002e
    '''                       NRP-Z27  : 0x002f
    '''                       NRP-Z28  : 0x0051
    '''                       NRP-Z98  : 0x0052
    '''                       NRP-Z92  : 0x0062
    '''                       NRP-Z57  : 0x0070
    '''                       NRP-Z85  : 0x0083
    '''                       NRPC40   : 0x008f
    '''                       NRPC50   : 0x0090
    '''                       NRP-Z86  : 0x0095
    '''                       NRP-Z211 : 0x00a6
    '''                       NRP-Z221 : 0x00a7
    '''                       NRP-Z58  : 0x00a8
    '''                       NRPC33   : 0x00b6
    '''                       NRPC18   : 0x00bf
    '''
    '''  serial number you can find on your sensor. Serial number is number with 6 digits. For example 9000003
    '''
    ''' you can use star '*' for product id or serial number in resource descriptor. Use star only for one connected sensor, because driver opens only first sensor on the bus.
    ''' 
    '''The USB keyword is used for USB interface.
    '''
    '''Examples:
    '''USB   - "USB::0x0aad::0x000b::100000"
    '''USB   - "USB::0x0aad::0x000b::*" - Opens first FSH-Z1 sensor
    '''USB   - "USB::0x0aad::*"         - Opens first R&amp;S sensor
    '''USB   - "*"                      - Opens first R&amp;S sensor
    '''</param>
    '''<param name="ID_Query">
    '''This control specifies if an ID Query is sent to the instrument during the initialization procedure.
    '''
    '''Valid Range:
    '''VI_FALSE (0) - Skip Query
    '''VI_TRUE  (1) - Do Query (Default Value)
    '''
    '''Notes:
    '''   
    '''(1) Under normal circumstances the ID Query ensures that the instrument initialized is the type supported by this driver. However circumstances may arise where it is undesirable to send an ID Query to the instrument.  In those cases; set this control to "Skip Query" and this function will initialize the selected interface, without doing an ID Query.
    '''
    '''</param>
    '''<param name="Reset_Device">
    '''This control specifies if the instrument is to be reset to its power-on settings during the initialization procedure.
    '''
    '''Valid Range:
    '''VI_FALSE (0) - Don't Reset
    '''VI_TRUE  (1) - Reset Device (Default Value)
    '''
    '''Notes:
    '''
    '''(1) If you do not want the instrument reset. Set this control to "Don't Reset" while initializing the instrument.
    '''
    '''</param>
    Public Sub New(ByVal Resource_Name As String, ByVal ID_Query As Boolean, ByVal Reset_Device As Boolean)
        MyBase.New
        Dim instrumentHandle As System.IntPtr
        Dim pInvokeResult As Integer = PInvoke.init(Resource_Name, System.Convert.ToUInt16(ID_Query), System.Convert.ToUInt16(Reset_Device), instrumentHandle)
        Me._handle = New System.Runtime.InteropServices.HandleRef(Me, instrumentHandle)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Me._disposed = false
    End Sub
    
    '''<summary>
    '''This function performs the following initialization actions and assigned specified sesnor to channel 1:
    '''
    '''- Opens a session to the specified device using the interface and address specified in the Resource Name control.
    '''
    '''- Performs an identification query on the sensor.
    '''
    '''- Resets the instrument to a known state.
    '''
    '''- Sends initialization commands to the sensor
    '''
    '''- Returns an Instrument Handle which is used to differentiate between different sessions of this instrument driver.
    '''</summary>
    '''<param name="ID_Query">
    '''This control specifies if an ID Query is sent to the instrument during the initialization procedure.
    '''
    '''Valid Range:
    '''VI_FALSE (0) - Skip Query
    '''VI_TRUE  (1) - Do Query (Default Value)
    '''
    '''Notes:
    '''   
    '''(1) Under normal circumstances the ID Query ensures that the instrument initialized is the type supported by this driver. However circumstances may arise where it is undesirable to send an ID Query to the instrument.  In those cases; set this control to "Skip Query" and this function will initialize the selected interface, without doing an ID Query.
    '''
    '''</param>
    '''<param name="Port">
    '''This control selects the port.
    '''
    '''Valid Values:
    '''RSNRPZ_Z5_PORT_A (0) - A
    '''RSNRPZ_Z5_PORT_B (1) - B
    '''RSNRPZ_Z5_PORT_C (2) - C
    '''RSNRPZ_Z5_PORT_D (3) - D
    '''
    '''Default Value: RSNRPZ_Z5_PORT_A (0)
    '''</param>
    '''<param name="Reset_Device">
    '''This control specifies if the instrument is to be reset to its power-on settings during the initialization procedure.
    '''
    '''Valid Range:
    '''VI_FALSE (0) - Don't Reset
    '''VI_TRUE  (1) - Reset Device (Default Value)
    '''
    '''Notes:
    '''
    '''(1) If you do not want the instrument reset. Set this control to "Don't Reset" while initializing the instrument.
    '''
    '''</param>
    Public Sub New(ByVal ID_Query As Boolean, ByVal Port As Integer, ByVal Reset_Device As Boolean)
        MyBase.New
        Dim instrumentHandle As System.IntPtr
        Dim pInvokeResult As Integer = PInvoke.initZ5(System.Convert.ToUInt16(ID_Query), Port, System.Convert.ToUInt16(Reset_Device), instrumentHandle)
        Me._handle = New System.Runtime.InteropServices.HandleRef(Me, instrumentHandle)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Me._disposed = false
    End Sub
    
    '''<summary>
    '''Gets the instrument handle.
    '''</summary>
    '''<value>
    '''The value is the IntPtr that represents the handle to the instrument.
    '''</value>
    Public ReadOnly Property Handle() As System.IntPtr
        Get
            Return Me._handle.Handle
        End Get
    End Property
    
    '''<summary>
    '''This function performs the same initialization as the
    '''rsnrpz_init() function (see there), but should be used
    '''when the sensor(s) are connected via an extension unit
    '''like, for example, AnywhereUSB
    '''
    '''
    '''Notes:
    '''
    '''1) Never use both the rsnrpz_init() and
    '''   rsnrpz_init_long_distance() funtions concurrently
    '''   Locally connected sensors can also be used with a
    '''   session ID returned by the 'long distance' version
    '''   of init
    '''
    '''2) Do not initilize every sensor with a rsnrpz_init...
    '''   function. If you want comunicate with more than one
    '''   sensor use rsnrpz_AddSensor() for adding a new channel.
    '''   The reason is that the rsnrpz_close() function
    '''   destroys all sensor sessions assigned to a process.
    '''</summary>
    '''<param name="ID_Query">
    '''This control specifies if an ID Query is sent to the instrument during the initialization procedure.
    '''
    '''Valid Range:
    '''VI_FALSE (0) - Skip Query
    '''VI_TRUE  (1) - Do Query (Default Value)
    '''
    '''Notes:
    '''   
    '''(1) Under normal circumstances the ID Query ensures that the instrument initialized is the type supported by this driver. However circumstances may arise where it is undesirable to send an ID Query to the instrument.  In those cases; set this control to "Skip Query" and this function will initialize the selected interface, without doing an ID Query.
    '''
    '''</param>
    '''<param name="Reset_Device">
    '''This control specifies if the instrument is to be reset to its power-on settings during the initialization procedure.
    '''
    '''Valid Range:
    '''VI_FALSE (0) - Don't Reset
    '''VI_TRUE  (1) - Reset Device (Default Value)
    '''
    '''Notes:
    '''
    '''(1) If you do not want the instrument reset. Set this control to "Don't Reset" while initializing the instrument.
    '''
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This driver defines the following status codes:
    '''
    '''Status    Description
    '''-------------------------------------------------
    '''BFFC0002  Parameter 2 (ID Query) out of range.
    '''BFFC0003  Parameter 3 (Reset Device) out of range.
    '''BFFC0011  Instrument returned invalid response to ID Query
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''   
    '''(1) Under normal circumstances the ID Query ensures that the instrument initialized is the type supported by this driver. However circumstances may arise where it is undesirable to send an ID Query to the instrument.  In those cases; set this control to "Skip Query" and this function will initialize the selected interface, without doing an ID Query.
    '''
    '''</returns>
    '''<param name="Port">
    '''This control selects the port.
    '''
    '''Valid Values:
    '''RSNRPZ_Z5_PORT_A (0) - A
    '''RSNRPZ_Z5_PORT_B (1) - B
    '''RSNRPZ_Z5_PORT_C (2) - C
    '''RSNRPZ_Z5_PORT_D (3) - D
    '''
    '''Default Value: RSNRPZ_Z5_PORT_A (0)
    '''</param>
    Public Shared Function initZ5(ByVal ID_Query As Boolean, ByVal Port As Integer, ByVal Reset_Device As Boolean) As rsnrpz
        Dim handle As System.IntPtr
        Dim pInvokeResult As Integer = PInvoke.initZ5(System.Convert.ToUInt16(ID_Query), Port, System.Convert.ToUInt16(Reset_Device), handle)
        PInvoke.TestForError(New System.Runtime.InteropServices.HandleRef(Nothing, System.IntPtr.Zero), pInvokeResult)
        Return New rsnrpz(handle)
    End Function
    
    '''<summary>
    '''This function immediately sets all the sensors to the IDLE state. Measurements in progress are interrupted. If INIT:CONT ON is set, a new measurement is immediately started since the trigger system is not influenced.
    '''
    '''Remote-control command(s):
    '''ABOR
    '''</summary>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function chans_abort() As Integer
        Dim pInvokeResult As Integer = PInvoke.chans_abort(Me._handle)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function returns number of available channels (1, 2 or 4 depending on installed options NRP-B2 - Two channel interface and NRP-B5 - Four channel interface).
    '''</summary>
    '''<param name="Count">
    '''This control returns number of available channels (1, 2 or 4 depending on installed options NRP-B2 - Two channel interface and NRP-B5 - Four channel interface).
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function chans_getCount(ByRef Count As Integer) As Integer
        Dim pInvokeResult As Integer = PInvoke.chans_getCount(Me._handle, Count)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function starts a single-shot measurement on all channels. The respective sensor goes to the INITIATED state. The command is completely executed when the sensor returns to the IDLE state. The command is ignored when the sensor is not in the IDLE state or when continuous measurements are selected (INIT:CONT ON). The command is only fully executed when the measurement is completed and the trigger system has again reached the IDLE state. INIT is the only remote control command that permits overlapping execution. Other commands can be received and processed while the command is being executed.
    '''
    '''Remote-control command(s):
    '''STAT:OPER:MEAS?
    '''INIT:IMM
    '''</summary>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function chans_initiate() As Integer
        Dim pInvokeResult As Integer = PInvoke.chans_initiate(Me._handle)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function starts zeroing of all sensors using the signal at each sensor input. Zeroing is an asynchronous operation which will require a couple of seconds. Therefore, after starting the function, the user should poll the current execution status by continuously calling rsnrpz_chans_isZeroComplete(). As soon as the zeroing has finished, the result of the operation can be queried by a call to rsnrpz_error_query(). See the example code below.
    '''
    '''Note: All sensors must be disconnected from all power sources. If the signal at the input considerably deviates from 0 W, the sensor aborts the zero calibration and raises an error condition. The rsnrpz driver queues the error for later retrieval by the rsnrpz_error_query() function.
    '''
    '''Example code
    '''bool Zero( ViSession lSesID )
    '''{
    '''  const int CH1 = 1;
    '''  ViStatus lStat = VI_SUCCESS;
    '''  ViBoolean bZeroComplete = VI_FALSE;
    '''  ViInt32 iErrorCode = VI_SUCCESS;
    '''  ViChar szErrorMsg[256];
    '''  /* Start zeroing the sensor  */
    '''  lStat = rsnrpz_chans_zero( lSesID );
    '''  if ( lStat != VI_SUCCESS )
    '''  {
    '''    fprintf( stderr, "Error 0x%08x in rsnrpz_chan_zero()", lStat );
    '''    return false;
    '''  }
    '''  while ( bZeroComplete == VI_FALSE )
    '''  {
    '''    lStat = rsnrpz_chans_isZeroComplete( lSesID, &amp;bZeroComplete );
    '''    if ( bZeroComplete )
    '''    {
    '''      rsnrpz_error_query( lSesID, &amp;iErrorCode, szErrorMsg );
    '''      fprintf( stderr, "Zero-Cal.: error %d, %s\n\n", iErrorCode, szErrorMsg );
    '''      break;
    '''    }
    '''    else 
    '''      SLEEP( 200 );
    '''  }
    '''  return iErrorCode == 0;
    '''}
    '''
    '''Remote-control command(s):
    '''CAL:ZERO:AUTO
    '''</summary>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function chans_zero() As Integer
        Dim pInvokeResult As Integer = PInvoke.chans_zero(Me._handle)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function should be used for polling whether a previously started zero calibration on a group of sensor has already finished. Zero calibration is an asynchronous operation and may take some seconds until completion. See the example code under rsnrpz_chans_zero() on how to conduct a zeroing calibration on a group of sensors.
    '''</summary>
    '''<param name="Zeroing_Completed">
    '''This control returns VI_TRUE if all channels have calibration ready.
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function chans_isZeroingComplete(ByRef Zeroing_Completed As Boolean) As Integer
        Dim Zeroing_CompletedAsUShort As UShort
        Dim pInvokeResult As Integer = PInvoke.chans_isZeroingComplete(Me._handle, Zeroing_CompletedAsUShort)
        Zeroing_Completed = System.Convert.ToBoolean(Zeroing_CompletedAsUShort)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function returns the summary status of measurements on all channels.
    '''</summary>
    '''<param name="Measurement_Completed">
    '''This control returns VI_TRUE if all channels have measurement ready.
    '''
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function chans_isMeasurementComplete(ByRef Measurement_Completed As Boolean) As Integer
        Dim Measurement_CompletedAsUShort As UShort
        Dim pInvokeResult As Integer = PInvoke.chans_isMeasurementComplete(Me._handle, Measurement_CompletedAsUShort)
        Measurement_Completed = System.Convert.ToBoolean(Measurement_CompletedAsUShort)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function sets the sensor to one of the measurement modes.
    '''
    '''Remote-control command(s):
    '''SENSe[1..4]:FUNCtion
    '''SENSe[1..4]:BUFFer:STATe ON | OFF
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Measurement_Mode">
    '''This control specifies the measurement mode to which sensor should be switched.
    '''
    '''Valid Values:
    '''RSNRPZ_SENSOR_MODE_CONTAV (0) - ContAv (Default Value)
    '''RSNRPZ_SENSOR_MODE_BUF_CONTAV (1) - Buffered ContAv
    '''RSNRPZ_SENSOR_MODE_TIMESLOT (2) - Timeslot
    '''RSNRPZ_SENSOR_MODE_BURST (3) - Burst
    '''RSNRPZ_SENSOR_MODE_SCOPE (4) - Scope
    '''RSNRPZ_SENSOR_MODE_CCDF (6) - CCDF
    '''RSNRPZ_SENSOR_MODE_PDF (7) - PDF
    '''
    '''Notes:
    '''(1) ContAv: After a trigger event, the power is integrated over a time interval (Averaging).
    '''
    '''(2) Buffered ContAv: The same as ContAv except the buffered mode is switched On.
    '''
    '''(3) Timeslot: The power is measured simultaneously in a number of timeslots (up to 26).
    '''
    '''(4) Burst: SENS:POW:BURS:DTOL defines the time interval during which a signal drop below the trigger level is not interpreted as the end of the burst. In the BurstAv mode, the set trigger source is ignored.
    '''
    '''(5) Scope: A sequence of measurements is performed. The individual measured values are determined as in the ContAv mode.
    '''
    '''(6) NRP-Z51 supports only RSNRPZ_SENSOR_MODE_CONTAV and RSNRPZ_SENSOR_MODE_BUF_CONTAV.
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function chan_mode(ByVal Channel As Integer, ByVal Measurement_Mode As Integer) As Integer
        Dim pInvokeResult As Integer = PInvoke.chan_mode(Me._handle, Channel, Measurement_Mode)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function configures times that is to be excluded at the beginning and at the end of the integration.
    '''
    '''Note:
    '''  
    '''1) This function is not available for NRP-Z51.
    '''
    '''Remote-control command(s):
    '''SENS:TIM:EXCL:STAR
    '''SENS:TIM:EXCL:STOP
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Exclude_Start">
    '''This control sets a time (in seconds) that is to be excluded at the beginning of the integration
    '''
    '''Valid Range:
    '''NRP-Z21: 0.0 - 0.1 s
    '''FSH-Z1:  0.0 - 0.1 s
    '''
    '''
    '''Default Value:
    '''0.0 s
    '''
    '''Notes:
    '''(1) Actual valid range depends on sensor used
    '''</param>
    '''<param name="Exclude_Stop">
    '''This control sets a time (in seconds) that is to be excluded at the end of the integration.
    '''
    '''Valid Range:
    '''NRP-Z21: 0.0 - 0.003 s
    '''FSH-Z1:  0.0 - 0.003 s
    '''
    '''Default Value:
    '''0.0 s
    '''
    '''Notes:
    '''(1) Actual valid range depends on sensor used
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function timing_configureExclude(ByVal Channel As Integer, ByVal Exclude_Start As Double, ByVal Exclude_Stop As Double) As Integer
        Dim pInvokeResult As Integer = PInvoke.timing_configureExclude(Me._handle, Channel, Exclude_Start, Exclude_Stop)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function sets a time that is to be excluded at the beginning of the integration.
    '''
    '''Note:
    '''  
    '''1) This function is not available for NRP-Z51.
    '''
    '''Remote-control command(s):
    '''SENS:TIM:EXCL:STAR
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Exclude_Start">
    '''This control sets a time (in seconds) that is to be excluded at the beginning of the integration
    '''
    '''Valid Range:
    '''NRP-Z21: 0.0 - 0.1 s
    '''FSH-Z1:  0.0 - 0.1 s
    '''
    '''Default Value:
    '''0.0 s
    '''
    '''Notes:
    '''(1) Actual valid range depends on sensor used
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function timing_setTimingExcludeStart(ByVal Channel As Integer, ByVal Exclude_Start As Double) As Integer
        Dim pInvokeResult As Integer = PInvoke.timing_setTimingExcludeStart(Me._handle, Channel, Exclude_Start)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function reads a time that is to be excluded at the beginning of the integration.
    '''
    '''Note:
    '''  
    '''1) This function is not available for NRP-Z51.
    '''
    '''Remote-control command(s):
    '''SENS:TIM:EXCL:STAR?
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Exclude_Start">
    '''This control returns a time (in seconds) that is to be excluded at the beginning of the integration.
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function timing_getTimingExcludeStart(ByVal Channel As Integer, ByRef Exclude_Start As Double) As Integer
        Dim pInvokeResult As Integer = PInvoke.timing_getTimingExcludeStart(Me._handle, Channel, Exclude_Start)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function sets a time that is to be excluded at the end of the integration.
    '''
    '''Note:
    '''  
    '''1) This function is not available for NRP-Z51.
    '''
    '''Remote-control command(s):
    '''SENS:TIM:EXCL:STOP
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Exclude_Stop">
    '''This control sets a time (in seconds) that is to be excluded at the end of the integration.
    '''
    '''Valid Range:
    '''
    '''NRP-Z21: 0.0 - 0.003 s
    '''FSH-Z1:  0.0 - 0.003 s
    '''
    '''Default Value:
    '''0.0 s
    '''
    '''Notes:
    '''(1) Actual valid range depends on sensor used
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function timing_setTimingExcludeStop(ByVal Channel As Integer, ByVal Exclude_Stop As Double) As Integer
        Dim pInvokeResult As Integer = PInvoke.timing_setTimingExcludeStop(Me._handle, Channel, Exclude_Stop)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function reads a time that is to be excluded at the end of the integration.
    '''
    '''Note:
    '''  
    '''1) This function is not available for NRP-Z51.
    '''
    '''Remote-control command(s):
    '''SENS:TIM:EXCL:STOP?
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Exclude_Stop">
    '''This control returns a time (in seconds) that is to be excluded at the end of the integration.
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function timing_getTimingExcludeStop(ByVal Channel As Integer, ByRef Exclude_Stop As Double) As Integer
        Dim pInvokeResult As Integer = PInvoke.timing_getTimingExcludeStop(Me._handle, Channel, Exclude_Stop)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function can be used to reduce the video bandwidth for the Trace and Statistics modes. As a result, trigger sensitivity is increased and the display noise reduced. To prevent signals from being corrupted, the selected video bandwidth should not be smaller than the RF bandwidth of the measurement signal. The "FULL" setting corresponds to a video bandwidth of at least 30 MHz if there is an associated frequency setting (SENSe:FREQuency command) greater than or equal to 500 MHz. If a frequency below 500 MHz is set and the video bandwidth is set to "FULL", the video bandwidth is automatically reduced to approx. 7.5 MHz.
    '''If the video bandwidth is limited with the SENSe:BWIDth:VIDEo command, the sampling rate is also automatically reduced, i.e. the effective time resolution in the Trace mode is reduced accordingly. In the Statistics modes, the measurement time must be increased appropriately if the required sample size is to be maintained:
    '''Video bandwidth Sampling rate   Sampling interval
    '''"Full"            8e7 1/s       12.5 ns
    '''"5 MHz"           4e7 1/s         25 ns
    '''"1.5 MHz"         1e7 1/s        100 ns
    '''"300 kHz"       2.5e6 1/s        400 ns
    '''
    '''
    '''
    '''
    '''
    '''
    '''
    '''
    '''sets the video bandwidth according to a list of available bandwidths. The list can be obtained using function rsnrpz_bandwidth_getBwList.
    '''
    '''Remote-control command(s):
    '''SENSe:BWIDth:VIDeo
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Bandwidth">
    '''This control sets the video bandwidth according to a list of available bandwidths. The list can be obtained using function rsnrpz_bandwidth_getBwList.
    '''
    '''Valid Range:
    '''Index of bandwidth in the list
    '''
    '''Default Value:
    '''0
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function bandwidth_setBw(ByVal Channel As Integer, ByVal Bandwidth As Integer) As Integer
        Dim pInvokeResult As Integer = PInvoke.bandwidth_setBw(Me._handle, Channel, Bandwidth)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function queries selected video bandwidth.
    '''
    '''Remote-control command(s):
    '''SENSe:BWIDth:VIDeo?
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Bandwidth">
    '''This control returns selected video bandwidth as index in bandwidth list.
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function bandwidth_getBw(ByVal Channel As Integer, ByRef Bandwidth As Integer) As Integer
        Dim pInvokeResult As Integer = PInvoke.bandwidth_getBw(Me._handle, Channel, Bandwidth)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function queries the list of possible video bandwidths.
    '''
    '''Remote-control command(s):
    '''SENSe:BWIDth:VIDeo:LIST?
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Buffer_Size">
    '''This control defines the size of buffer passed to Bandwidth List argument.
    '''
    '''Valid Range:
    '''&gt; 0
    '''
    '''Default Value:
    '''200
    '''</param>
    '''<param name="Bandwidth_List">
    '''This control returns the comma separated list of possible video bandwidths.
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function bandwidth_getBwList(ByVal Channel As Integer, ByVal Buffer_Size As Integer, ByVal Bandwidth_List As System.Text.StringBuilder) As Integer
        Dim pInvokeResult As Integer = PInvoke.bandwidth_getBwList(Me._handle, Channel, Buffer_Size, Bandwidth_List)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function configures all parameters necessary for automatic detection of filter bandwidth.
    '''
    '''Remote-control command(s):
    '''SENS:AVER:COUN:AUTO ON
    '''SENS:AVER:COUN:AUTO:TYPE RES
    '''SENS:AVER:COUN:AUTO:RES &lt;value&gt;
    '''SENS:AVER:TCON REP
    '''
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Resolution">
    '''This control defines the number of significant places for linear units and the number of decimal places for logarithmic units which should be free of noise in the measurement result.
    '''
    '''Valid Range:
    '''1 to 4
    '''
    '''Default Value: 3
    '''
    '''Notes:
    '''(1) Actual range depend on sensor used and may vary from the range stated above.
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function avg_configureAvgAuto(ByVal Channel As Integer, ByVal Resolution As Integer) As Integer
        Dim pInvokeResult As Integer = PInvoke.avg_configureAvgAuto(Me._handle, Channel, Resolution)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function configures all parameters necessary for setting the noise ratio in the measurement result and automatic detection of filter bandwidth.
    '''
    '''Remote-control command(s):
    '''SENS:AVER:COUN:AUTO ON
    '''SENS:AVER:COUN:AUTO:TYPE NSR
    '''SENS:AVER:COUN:AUTO:NSR &lt;value&gt;
    '''SENS:AVER:COUN:AUTO:MTIM &lt;value&gt;
    '''SENS:AVER:TCON REP
    '''
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Maximum_Noise_Ratio">
    '''This control sets the maximum noise ratio in the measurement result. The value is set in dB.
    '''
    '''Valid Range:
    '''
    '''NRP-Z21: 0.0 - 1.0
    '''FSH-Z1:  0.0 - 1.0
    '''
    '''Default Value: 0.1
    '''
    '''Notes:
    '''(1) This value is not range checked; the actual range depends on sensor used.
    '''</param>
    '''<param name="Upper_Time_Limit">
    '''This control sets the upper time limit (maximum time to fill the filter).
    '''
    '''Valid Range:
    '''
    '''NRP-21: 0.01 - 999.99
    '''FSH-Z1: 0.01 - 999.99
    '''
    '''Default Value: 4.0
    '''
    '''Notes:
    '''(1) This value is not range checked, the actual range depends on sensor used.
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function avg_configureAvgNSRatio(ByVal Channel As Integer, ByVal Maximum_Noise_Ratio As Double, ByVal Upper_Time_Limit As Double) As Integer
        Dim pInvokeResult As Integer = PInvoke.avg_configureAvgNSRatio(Me._handle, Channel, Maximum_Noise_Ratio, Upper_Time_Limit)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function configures all parameters necessary for manual setting of filter bandwidth.
    '''
    '''Remote-control command(s):
    '''SENS:AVER:COUN
    '''SENS:AVER:COUN:AUTO OFF
    '''SENS:AVER:TCON REP
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Count">
    '''This control sets the filter bandwidth.
    '''
    '''Valid Range:
    '''1 - 65536
    '''
    '''Default Value: 4
    '''
    '''Notes:
    '''(1) Actual valid range depends on sensor used
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function avg_configureAvgManual(ByVal Channel As Integer, ByVal Count As Integer) As Integer
        Dim pInvokeResult As Integer = PInvoke.avg_configureAvgManual(Me._handle, Channel, Count)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function can be used to automatically determine a value for filter bandwidth.
    '''
    '''Remote-control command(s):
    '''SENS:AVER:COUN:AUTO ON|OFF
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Auto_Enabled">
    '''This control activates or deactivates automatic determination of a value for filter bandwidth.
    '''If the automatic switchover is activated with the ON parameter, the sensor always defines a suitable filter length.
    '''
    '''Valid Values:
    '''VI_FALSE (0) - Off
    '''VI_TRUE (1) - On (Default Value)
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function avg_setAutoEnabled(ByVal Channel As Integer, ByVal Auto_Enabled As Boolean) As Integer
        Dim pInvokeResult As Integer = PInvoke.avg_setAutoEnabled(Me._handle, Channel, System.Convert.ToUInt16(Auto_Enabled))
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function queries the setting of automatic switchover of filter bandwidth.
    '''
    '''Remote-control command(s):
    '''SENS:AVER:COUN:AUTO?
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Auto_Enabled">
    '''This control returns the setting of automatic switchover of filter bandwidth.
    '''
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function avg_getAutoEnabled(ByVal Channel As Integer, ByRef Auto_Enabled As Boolean) As Integer
        Dim Auto_EnabledAsUShort As UShort
        Dim pInvokeResult As Integer = PInvoke.avg_getAutoEnabled(Me._handle, Channel, Auto_EnabledAsUShort)
        Auto_Enabled = System.Convert.ToBoolean(Auto_EnabledAsUShort)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    ''' This function sets an upper time limit can be set via (maximum time). It should never be exceeded. Undesired long measurement times can thus be prevented if the automatic filter length switchover is on.
    '''
    '''Remote-control command(s):
    '''SENS:AVER:COUN:AUTO:MTIM
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Upper_Time_Limit">
    '''This control sets the upper time limit (maximum time to fill the filter).
    '''
    '''Valid Range:
    '''
    '''NRP-21: 0.01 - 999.99
    '''FSH-Z1: 0.01 - 999.99
    '''
    '''Default Value: 4.0
    '''
    '''Notes:
    '''(1) This value is not range checked, the actual range depends on sensor used.
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function avg_setAutoMaxMeasuringTime(ByVal Channel As Integer, ByVal Upper_Time_Limit As Double) As Integer
        Dim pInvokeResult As Integer = PInvoke.avg_setAutoMaxMeasuringTime(Me._handle, Channel, Upper_Time_Limit)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function queries an upper time limit (maximum time to fill the filter).
    '''
    '''Remote-control command(s):
    '''SENS:AVER:COUN:AUTO:MTIM?
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Upper_Time_Limit">
    '''This control returns an upper time limit (maximum time to fill the filter).
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function avg_getAutoMaxMeasuringTime(ByVal Channel As Integer, ByRef Upper_Time_Limit As Double) As Integer
        Dim pInvokeResult As Integer = PInvoke.avg_getAutoMaxMeasuringTime(Me._handle, Channel, Upper_Time_Limit)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function sets the maximum noise ratio in the measurement result.
    '''
    '''Remote-control command(s):
    '''SENS:AVER:COUN:AUTO:NSR
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Maximum_Noise_Ratio">
    '''This control sets the maximum noise ratio in the measurement result. The value is set in dB.
    '''
    '''Valid Range:
    '''
    '''NRP-Z21: 0.0 - 1.0
    '''FSH-Z1:  0.0 - 1.0
    '''
    '''Default Value: 0.1
    '''
    '''Notes:
    '''(1) This value is not range checked; the actual range depends on sensor used.
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function avg_setAutoNoiseSignalRatio(ByVal Channel As Integer, ByVal Maximum_Noise_Ratio As Double) As Integer
        Dim pInvokeResult As Integer = PInvoke.avg_setAutoNoiseSignalRatio(Me._handle, Channel, Maximum_Noise_Ratio)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function queries the maximum noise signal ratio value.
    '''
    '''Remote-control command(s):
    '''SENS:AVER:COUN:AUTO:NSR?
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Maximum_Noise_Ratio">
    '''This control returns a maximum noise signal ratio in dB.
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function avg_getAutoNoiseSignalRatio(ByVal Channel As Integer, ByRef Maximum_Noise_Ratio As Double) As Integer
        Dim pInvokeResult As Integer = PInvoke.avg_getAutoNoiseSignalRatio(Me._handle, Channel, Maximum_Noise_Ratio)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function defines the number of significant places for linear units and the number of decimal places for logarithmic units which should be free of noise in the measurement result. This setting does not affect the setting of display.
    '''
    '''Remote-control command(s):
    '''SENS:AVER:COUN:AUTO:RES 1 ... 4
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Resolution">
    '''This control defines the number of significant places for linear units and the number of decimal places for logarithmic units which should be free of noise in the measurement result.
    '''
    '''Valid Range:
    '''1 to 4
    '''
    '''Default Value: 3
    '''
    '''Notes:
    '''(1) Actual range depend on sensor used and may vary from the range stated above.
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function avg_setAutoResolution(ByVal Channel As Integer, ByVal Resolution As Integer) As Integer
        Dim pInvokeResult As Integer = PInvoke.avg_setAutoResolution(Me._handle, Channel, Resolution)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function returns the number of significant places for linear units and the number of decimal places for logarithmic units which should be free of noise in the measurement result. 
    '''
    '''Remote-control command(s):
    '''SENS:AVER:COUN:AUTO:RES?
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Resolution">
    '''This control returns the number of significant places for linear units and the number of decimal places for logarithmic units which should be free of noise in the measurement result.
    '''
    '''Valid Range:
    '''1 to 4
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function avg_getAutoResolution(ByVal Channel As Integer, ByRef Resolution As Integer) As Integer
        Dim pInvokeResult As Integer = PInvoke.avg_getAutoResolution(Me._handle, Channel, Resolution)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function selects a method by which the automatic filter length switchover can operate.
    '''
    '''Remote-control command(s):
    '''SENS:AVER:COUN:AUTO:TYPE RES | NSR
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Method">
    '''This control selects a method by which the automatic filter length switchover can operate.
    '''
    '''Valid Values:
    '''RSNRPZ_AUTO_COUNT_TYPE_RESOLUTION (0) - Resolution (Default Value)
    '''RSNRPZ_AUTO_COUNT_TYPE_NSR (1) - Noise Ratio
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function avg_setAutoType(ByVal Channel As Integer, ByVal Method As Integer) As Integer
        Dim pInvokeResult As Integer = PInvoke.avg_setAutoType(Me._handle, Channel, Method)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function returns a method by which the automatic filter length switchover can operate.
    '''
    '''Remote-control command(s):
    '''SENS:AVER:COUN:AUTO:TYPE?
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Method">
    '''This control returns a method by which the automatic filter length switchover can operate.
    '''
    '''Valid Values:
    '''Resolution (RSNRPZ_AUTO_COUNT_TYPE_RESOLUTION)
    '''Noise Ratio (RSNRPZ_AUTO_COUNT_TYPE_NSR)
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function avg_getAutoType(ByVal Channel As Integer, ByRef Method As Integer) As Integer
        Dim pInvokeResult As Integer = PInvoke.avg_getAutoType(Me._handle, Channel, Method)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function sets the filter bandwidth. The wider the filter the lower the noise and the longer it takes to obtain a measured value.
    '''
    '''Remote-control command(s):
    '''SENS:AVER:COUN
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Count">
    '''This control sets the filter bandwidth.
    '''
    '''Valid Range:
    '''1 - 65536
    '''
    '''Default Value: 4
    '''
    '''Notes:
    '''(1) Actual valid range depends on sensor used
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function avg_setCount(ByVal Channel As Integer, ByVal Count As Integer) As Integer
        Dim pInvokeResult As Integer = PInvoke.avg_setCount(Me._handle, Channel, Count)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function returns the filter bandwidth.
    '''
    '''Remote-control command(s):
    '''SENS:AVER:COUN?
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Count">
    '''This control returns the filter bandwidth.
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function avg_getCount(ByVal Channel As Integer, ByRef Count As Integer) As Integer
        Dim pInvokeResult As Integer = PInvoke.avg_getCount(Me._handle, Channel, Count)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function switches the filter function of a sensor on or off. When the filter is switched on, the number of measured values set with rsnrpz_avg_setCount() is averaged. This reduces the effect of noise so that more reliable results are obtained.
    '''
    '''Remote-control command(s):
    '''SENS:AVER:STAT ON | OFF
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Averaging">
    '''This control switches the filter function of a sensor on or off.
    '''
    '''Valid Values:
    '''VI_TRUE (1)  - On (Default Value)
    '''VI_FALSE (0) - Off
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function avg_setEnabled(ByVal Channel As Integer, ByVal Averaging As Boolean) As Integer
        Dim pInvokeResult As Integer = PInvoke.avg_setEnabled(Me._handle, Channel, System.Convert.ToUInt16(Averaging))
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function returns the state of the filter function of a sensor.
    '''
    '''Remote-control command(s):
    '''SENS:AVER:STAT?
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Averaging">
    '''This control returns the state of the filter function of a sensor.
    '''
    '''Valid Values:
    '''VI_TRUE (1) - On
    '''VI_FALSE (0) - Off
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function avg_getEnabled(ByVal Channel As Integer, ByRef Averaging As Boolean) As Integer
        Dim AveragingAsUShort As UShort
        Dim pInvokeResult As Integer = PInvoke.avg_getEnabled(Me._handle, Channel, AveragingAsUShort)
        Averaging = System.Convert.ToBoolean(AveragingAsUShort)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function sets a timeslot whose measured value is used to automatically determine the filter length.
    '''
    '''Note:
    '''
    '''1) This function is not available for NRP-Z51.
    '''
    '''Remote-control command(s):
    '''SENS:AVER:COUN:AUTO:SLOT
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Timeslot">
    '''This control sets a timeslot whose measured value is used to automatically determine the filter length.
    '''
    '''Valid Range:
    '''
    '''NRP-Z21: 1 - 8
    '''FSH-Z1:  1 - 8
    '''
    '''Default Value: 1
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function avg_setSlot(ByVal Channel As Integer, ByVal Timeslot As Integer) As Integer
        Dim pInvokeResult As Integer = PInvoke.avg_setSlot(Me._handle, Channel, Timeslot)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function returns a timeslot whose measured value is used to automatically determine the filter length.
    '''
    '''Note:
    '''
    '''1) This function is not available for NRP-Z51.
    '''
    '''Remote-control command(s):
    '''SENS:AVER:COUN:AUTO:SLOT?
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Timeslot">
    '''This control returns a timeslot whose measured value is used to automatically determine the filter length.
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function avg_getSlot(ByVal Channel As Integer, ByRef Timeslot As Integer) As Integer
        Dim pInvokeResult As Integer = PInvoke.avg_getSlot(Me._handle, Channel, Timeslot)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function determines whether a new result is calculated immediately after a new measured value is available or only after an entire range of new values is available for the filter.
    '''
    '''Remote-control command(s):
    '''SENS:AVER:TCON MOV | REP
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Terminal_Control">
    '''This control determines the type of terminal control.
    '''
    '''Valid Values:
    '''RSNRPZ_TERMINAL_CONTROL_MOVING (0) - Moving
    '''RSNRPZ_TERMINAL_CONTROL_REPEAT (1) - Repeat (Default Value)
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function avg_setTerminalControl(ByVal Channel As Integer, ByVal Terminal_Control As Integer) As Integer
        Dim pInvokeResult As Integer = PInvoke.avg_setTerminalControl(Me._handle, Channel, Terminal_Control)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function returns the type of terminal control.
    '''
    '''Remote-control command(s):
    '''SENSe[1..4]:AVERage:TCONtrol?
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Terminal_Control">
    '''This control returns the type of terminal control.
    '''
    '''Valid Values:
    '''RSNRPZ_TERMINAL_CONTROL_MOVING (0) - Moving
    '''RSNRPZ_TERMINAL_CONTROL_REPEAT (1) - Repeat (Default Value)
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function avg_getTerminalControl(ByVal Channel As Integer, ByRef Terminal_Control As Integer) As Integer
        Dim pInvokeResult As Integer = PInvoke.avg_getTerminalControl(Me._handle, Channel, Terminal_Control)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function initializes the digital filter by deleting the stored measured values.
    '''
    '''Remote-control command(s):
    '''SENS:AVER:RES
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function avg_reset(ByVal Channel As Integer) As Integer
        Dim pInvokeResult As Integer = PInvoke.avg_reset(Me._handle, Channel)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function sets the automatic selection of a measurement range to ON or OFF.
    '''
    '''Note:
    '''
    '''1) This function is not available for NRP-Z51.
    '''
    '''Remote-control command(s):
    '''SENS:RANG:AUTO ON | OFF
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Auto_Range">
    '''This control sets the automatic selection of a measurement range to ON or OFF.
    '''
    '''Valid Values:
    '''VI_TRUE (1)  - On (Default Value)
    '''VI_FALSE (0) - Off
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function range_setAutoEnabled(ByVal Channel As Integer, ByVal Auto_Range As Boolean) As Integer
        Dim pInvokeResult As Integer = PInvoke.range_setAutoEnabled(Me._handle, Channel, System.Convert.ToUInt16(Auto_Range))
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function returns the state of automatic selection of a measurement range.
    '''
    '''Note:
    '''
    '''1) This function is not available for NRP-Z51.
    '''
    '''Remote-control command(s):
    '''SENS:RANG:AUTO?
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Auto_Range">
    '''This control returns the state of automatic selection of a measurement range.
    '''
    '''Valid Values:
    '''VI_TRUE (1) - On
    '''VI_FALSE (0) - Off
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function range_getAutoEnabled(ByVal Channel As Integer, ByRef Auto_Range As Boolean) As Integer
        Dim Auto_RangeAsUShort As UShort
        Dim pInvokeResult As Integer = PInvoke.range_getAutoEnabled(Me._handle, Channel, Auto_RangeAsUShort)
        Auto_Range = System.Convert.ToBoolean(Auto_RangeAsUShort)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function sets the cross-over level. Shifts the transition ranges between the measurement ranges. This may improve the measurement accuracy for special signals, i.e. signals with a high crest factor.
    '''
    '''Note:
    '''
    '''1) This function is not available for NRP-Z51.
    '''
    '''Remote-control command(s):
    '''SENS:RANG:AUTO:CLEV
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Crossover_Level">
    '''This control sets the cross-over level in dB.
    '''
    '''Valid Range:
    '''
    '''NRP-Z21: -20.0 - 0.0 dB
    '''FSH-Z1:  -20.0 - 0.0 dB
    '''
    '''Default Value: 0.0 dB
    '''
    '''Notes:
    '''(1) Actual valid range depends on sensor used
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function range_setCrossoverLevel(ByVal Channel As Integer, ByVal Crossover_Level As Double) As Integer
        Dim pInvokeResult As Integer = PInvoke.range_setCrossoverLevel(Me._handle, Channel, Crossover_Level)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function reads the cross-over level.
    '''
    '''Note:
    '''
    '''1) This function is not available for NRP-Z51.
    '''
    '''Remote-control command(s):
    '''SENS:RANG:AUTO:CLEV?
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Crossover_Level">
    '''This control returns the cross-over level in dB.
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors 
    '''</returns>
    Public Function range_getCrossoverLevel(ByVal Channel As Integer, ByRef Crossover_Level As Double) As Integer
        Dim pInvokeResult As Integer = PInvoke.range_getCrossoverLevel(Me._handle, Channel, Crossover_Level)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function selects a measurement range in which the corresponding sensor is to perform a measurement.
    '''
    '''Note:
    '''
    '''1) This function is not available for NRP-Z51.
    '''
    '''Remote-control command(s):
    '''SENS:RANG 0 .. 2
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Range">
    '''This control selects a measurement range in which the corresponding sensor is to perform a measurement.
    '''
    '''Valid Range:
    '''NRP-Z21:  0 to 2
    '''FSH-1:    0 to 2
    '''
    '''Default Value: 2
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function range_setRange(ByVal Channel As Integer, ByVal Range As Integer) As Integer
        Dim pInvokeResult As Integer = PInvoke.range_setRange(Me._handle, Channel, Range)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function returns a measurement range in which the corresponding sensor is to perform a measurement.
    '''
    '''Note:
    '''
    '''1) This function is not available for NRP-Z51
    '''
    '''Remote-control command(s):
    '''SENS:RANG?
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Range">
    '''This control returns a measurement range in which the corresponding sensor is to perform a measurement.
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function range_getRange(ByVal Channel As Integer, ByRef Range As Integer) As Integer
        Dim pInvokeResult As Integer = PInvoke.range_getRange(Me._handle, Channel, Range)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function configures all correction parameters.
    '''
    '''Remote-control command(s):
    '''SENS:CORR:OFFS
    '''SENS:CORR:OFFS:STAT ON | OFF
    '''SENS:CORR:SPD:STAT ON | OFF
    '''
    '''
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Offset_State">
    '''This control switches the offset correction on or off.
    '''
    '''Valid Values:
    '''VI_TRUE  (1) - On
    '''VI_FALSE (0) - Off (Default Value)
    '''</param>
    '''<param name="Offset">
    '''This control sets a fixed offset value can be defined for multiplying (logarithmically adding) the measured value of a sensor.
    '''
    '''Valid Range:
    '''  -200.0 to 200.0 dB
    '''
    '''
    '''Default Value:
    '''0.0 dB
    '''
    '''Notes:
    '''(1) Actual valid range depends on sensor used
    '''</param>
    '''<param name="Reserved_1">
    '''This prameter is reserved. Value is ignored.
    '''</param>
    '''<param name="Reserved_2">
    '''This prameter is reserved. Value is ignored.
    '''
    '''Default Value:
    '''""
    '''</param>
    '''<param name="S_Parameter_Enable">
    '''This control enables or disables measured-value correction by means of the stored s-parameter device.
    '''
    '''Valid Values:
    '''VI_TRUE  (1) - On
    '''VI_FALSE (0) - Off (Default Value)
    '''
    '''
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function corr_configureCorrections(ByVal Channel As Integer, ByVal Offset_State As Boolean, ByVal Offset As Double, ByVal Reserved_1 As Boolean, ByVal Reserved_2 As String, ByVal S_Parameter_Enable As Boolean) As Integer
        Dim pInvokeResult As Integer = PInvoke.corr_configureCorrections(Me._handle, Channel, System.Convert.ToUInt16(Offset_State), Offset, System.Convert.ToUInt16(Reserved_1), Reserved_2, System.Convert.ToUInt16(S_Parameter_Enable))
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function informs the R&amp;S NRP about the frequency of the power to be measured since this frequency is not automatically determined. The frequency is used to determine a frequency-dependent correction factor for the measurement results.
    '''
    '''Remote-control command(s):
    '''SENS:FREQ
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Frequency">
    '''This control sets the frequency in Hz of the power to be measured since this frequency is not automatically determined.
    '''
    '''Valid Range:
    '''
    '''NRP-Z21: 10.0e6 - 18.0e9
    '''FSH-Z1:  10.0e6 -  8.0e9
    '''NRP-Z51: 0.0    - 18.0e9 (depends on the calibration data)
    '''
    '''Default Value: 50.0e6 Hz
    '''
    '''Notes:
    '''(1) Actual valid range depends on sensor used.
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function chan_setCorrectionFrequency(ByVal Channel As Integer, ByVal Frequency As Double) As Integer
        Dim pInvokeResult As Integer = PInvoke.chan_setCorrectionFrequency(Me._handle, Channel, Frequency)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function queries the instrument for the frequency of the power to be measured since this frequency is not automatically determined.
    '''
    '''Remote-control command(s):
    '''SENSe[1..4]:FREQuency?
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Frequency">
    '''This control returns the frequency in Hz of the power to be measured since this frequency is not automatically determined.
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function chan_getCorrectionFrequency(ByVal Channel As Integer, ByRef Frequency As Double) As Integer
        Dim pInvokeResult As Integer = PInvoke.chan_getCorrectionFrequency(Me._handle, Channel, Frequency)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''If the frequency step parameter is set to a value greater than 0.0 the sensor does a internal frequency increment if buffered mode is enabled
    '''
    '''Depending on the parameter "frequency spacing" the sensor adds this value to the current frequency or it multiplies this value with the current frequency.
    '''
    '''This function is used to do a simple scalar network nalysis. 
    '''To enable this automativally frequency stepping you have to configure CONTAV sensor mode, enable buffered measurements and set frequency stepping to a value greater than 0.
    '''
    '''Remote-control command(s):
    '''SENS:FREQ:STEP
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Frequency_Step">
    '''This control sets the frequency step value.
    '''
    '''Valid Range:
    '''0 to 0.5 * MAXfreq
    '''
    '''Default Value: 0.0
    '''
    '''Notes:
    '''(1) Actual valid range depends on sensor used.
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function chan_setCorrectionFrequencyStep(ByVal Channel As Integer, ByVal Frequency_Step As Double) As Integer
        Dim pInvokeResult As Integer = PInvoke.chan_setCorrectionFrequencyStep(Me._handle, Channel, Frequency_Step)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function returns the frequency step value.
    '''
    '''Remote-control command(s):
    '''SENS:FREQ:STEP?
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Frequency_Step">
    '''This control returns the frequency step value.
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function chan_getCorrectionFrequencyStep(ByVal Channel As Integer, ByRef Frequency_Step As Double) As Integer
        Dim pInvokeResult As Integer = PInvoke.chan_getCorrectionFrequencyStep(Me._handle, Channel, Frequency_Step)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''If scalar network analysis is enabled this parameter defines how the frequency is incremented.
    '''
    '''Linear spacing means that the frequency step value is added to the current frequency after each buffered measurement.
    '''Logarithmic spacing means that the frequency step value is multiplied with the current frequency after each buffered measurement.
    '''
    '''This command is used to do a simple scalar network nalysis. 
    '''To enable this automativally frequency stepping you have to configure CONTAV sensor mode, enable buffered measurements and set frequency stepping to a value greater than 0.
    '''
    '''Remote-control command(s):
    '''SENS:FREQ:SPAC
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Frequency_Spacing">
    '''This control selects the frequency spacing value.
    '''
    '''Valid Range:
    '''RSNRPZ_SPACING_LINEAR (0) - Linear
    '''RSNRPZ_SPACING_LOG    (1) - Logarithmic
    '''
    '''Default Value: RSNRPZ_SPACING_LINEAR (0)
    '''
    '''Note(s):
    '''
    '''(1) Linear: linear increment of correction frequency (spacing is added).
    '''
    '''(2) Logarithmic: logarithmic increment of corrcetion frequency (spacing is multiplied).
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function chan_setCorrectionFrequencySpacing(ByVal Channel As Integer, ByVal Frequency_Spacing As Integer) As Integer
        Dim pInvokeResult As Integer = PInvoke.chan_setCorrectionFrequencySpacing(Me._handle, Channel, Frequency_Spacing)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function returns the frequency spacing value.
    '''
    '''Remote-control command(s):
    '''SENS:FREQ:SPAC?
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Frequency_Spacing">
    '''This control returns the frequency spacing value.
    '''
    '''Valid Range:
    '''RSNRPZ_SPACING_LINEAR (0) - Linear
    '''RSNRPZ_SPACING_LOG    (1) - Logarithmic
    '''
    '''Note(s):
    '''
    '''(1) Linear: linear increment of correction frequency (spacing is added).
    '''
    '''(2) Logarithmic: logarithmic increment of corrcetion frequency (spacing is multiplied).
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function chan_getCorrectionFrequencySpacing(ByVal Channel As Integer, ByRef Frequency_Spacing As Integer) As Integer
        Dim pInvokeResult As Integer = PInvoke.chan_getCorrectionFrequencySpacing(Me._handle, Channel, Frequency_Spacing)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''With this function a fixed offset value can be defined for multiplying (logarithmically adding) the measured value of a sensor.
    '''
    '''Remote-control command(s):
    '''SENS:CORR:OFFS
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Offset">
    '''This control sets a fixed offset value can be defined for multiplying (logarithmically adding) the measured value of a sensor.
    '''
    '''Valid Range:
    '''  -200.0 to 200.0 dB
    '''
    '''Default Value:
    '''0.0 dB
    '''
    '''Notes:
    '''(1) Actual valid range depends on sensor used
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function corr_setOffset(ByVal Channel As Integer, ByVal Offset As Double) As Integer
        Dim pInvokeResult As Integer = PInvoke.corr_setOffset(Me._handle, Channel, Offset)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function gets a fixed offset value defined for multiplying (logarithmically adding) the measured value of a sensor.
    '''
    '''Remote-control command(s):
    '''SENS:CORR:OFFS?
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Offset">
    '''This control returns a fixed offset value defined for multiplying (logarithmically adding) the measured value of a sensor.
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function corr_getOffset(ByVal Channel As Integer, ByRef Offset As Double) As Integer
        Dim pInvokeResult As Integer = PInvoke.corr_getOffset(Me._handle, Channel, Offset)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function switches the offset correction on or off.
    '''
    '''Remote-control command(s):
    '''SENS:CORR:OFFS:STAT ON | OFF
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Offset_State">
    '''This control switches the offset correction on or off.
    '''
    '''Valid Values:
    '''VI_TRUE (1)  - On 
    '''VI_FALSE (0) - Off (Default Value)
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function corr_setOffsetEnabled(ByVal Channel As Integer, ByVal Offset_State As Boolean) As Integer
        Dim pInvokeResult As Integer = PInvoke.corr_setOffsetEnabled(Me._handle, Channel, System.Convert.ToUInt16(Offset_State))
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function returns the offset correction on or off.
    '''
    '''Remote-control command(s):
    '''SENS:CORR:OFFS:STAT?
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Offset_State">
    '''This control returns the offset correction on or off.
    '''
    '''Valid Values:
    '''VI_TRUE (1) - On
    '''VI_FALSE (0) - Off
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function corr_getOffsetEnabled(ByVal Channel As Integer, ByRef Offset_State As Boolean) As Integer
        Dim Offset_StateAsUShort As UShort
        Dim pInvokeResult As Integer = PInvoke.corr_getOffsetEnabled(Me._handle, Channel, Offset_StateAsUShort)
        Offset_State = System.Convert.ToBoolean(Offset_StateAsUShort)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function instructs the sensor to perform a measured-value correction by means of the stored s-parameter device.
    '''
    '''Remote-control command(s):
    '''SENS:CORR:SPD:STAT ON | OFF
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="S_Parameter_Enable">
    '''This control enables or disables measured-value correction by means of the stored s-parameter device.
    '''
    '''Valid Values:
    '''VI_TRUE (1)  - On
    '''VI_FALSE (0) - Off (Default Value)
    '''
    '''
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function corr_setSParamDeviceEnabled(ByVal Channel As Integer, ByVal S_Parameter_Enable As Boolean) As Integer
        Dim pInvokeResult As Integer = PInvoke.corr_setSParamDeviceEnabled(Me._handle, Channel, System.Convert.ToUInt16(S_Parameter_Enable))
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function returns the state of a measured-value correction by means of the stored s-parameter device.
    '''
    '''Remote-control command(s):
    '''SENSe[1..4]:CORRection:SPDevice:STATe?
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="S_Parameter_Correction">
    '''This control returns the state of S-Parameter correction.
    '''
    '''Valid Values:
    '''VI_TRUE (1) - On
    '''VI_FALSE (0) - Off
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function corr_getSParamDeviceEnabled(ByVal Channel As Integer, ByRef S_Parameter_Correction As Boolean) As Integer
        Dim S_Parameter_CorrectionAsUShort As UShort
        Dim pInvokeResult As Integer = PInvoke.corr_getSParamDeviceEnabled(Me._handle, Channel, S_Parameter_CorrectionAsUShort)
        S_Parameter_Correction = System.Convert.ToBoolean(S_Parameter_CorrectionAsUShort)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function can be used to select a loaded data set for S-parameter correction. This data set is accessed by means of a consecutive number, starting with 1 for the first data set. If an invalid data set consecutive number is entered, an error message is output.
    '''
    '''Note(s):
    '''
    '''(1) This function is available only on NRP-Z81.
    '''
    '''Remote-control command(s):
    '''SENS:CORR:SPD:SEL
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="S_Parameter">
    '''This control can be used to select a loaded data set for S-parameter correction. This data set is accessed by means of a consecutive number, starting with 1 for the first data set. If an invalid data set consecutive number is entered, an error message is output.
    '''
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function corr_setSParamDevice(ByVal Channel As Integer, ByVal S_Parameter As Integer) As Integer
        Dim pInvokeResult As Integer = PInvoke.corr_setSParamDevice(Me._handle, Channel, S_Parameter)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function gets selected data set for S-parameter correction. 
    '''
    '''Note(s):
    '''
    '''(1) This function is available only on NRP-Z81.
    '''
    '''Remote-control command(s):
    '''SENS:CORR:SPD:SEL?
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="S_Parameter">
    '''This control returns selected data set for S-parameter correction. 
    '''
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function corr_getSParamDevice(ByVal Channel As Integer, ByRef S_Parameter As Integer) As Integer
        Dim pInvokeResult As Integer = PInvoke.corr_getSParamDevice(Me._handle, Channel, S_Parameter)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function gets list of S-Parameter devices. 
    '''
    '''Note(s):
    '''
    '''(1) This function is available only on NRP-Z81.
    '''
    '''Remote-control command(s):
    '''SENS:CORR:SPD:LIST?
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Buffer_Size">
    '''This control defines the size of buffer.
    '''
    '''Valid Range:
    '''not checked
    '''
    '''Default Value: 1000
    '''</param>
    '''<param name="S_Parameter_Device_List">
    '''This control returns selected data set for S-parameter correction. 
    '''
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function corr_getSParamDevList(ByVal Channel As Integer, ByVal Buffer_Size As Integer, ByVal S_Parameter_Device_List As System.Text.StringBuilder) As Integer
        Dim pInvokeResult As Integer = PInvoke.corr_getSParamDevList(Me._handle, Channel, Buffer_Size, S_Parameter_Device_List)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function sets the parameters of the reflection coefficient for measured-value correction.
    '''
    '''Remote-control command(s):
    '''SENS:SGAM
    '''SENS:SGAM:PHAS
    '''SENS:SGAM:CORR:STAT ON | OFF
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Source_Gamma_Correction">
    '''This control enables or disables source gamma correction of the measured value.
    '''
    '''Valid Values:
    '''VI_TRUE  (1) - On
    '''VI_FALSE (0) - Off (Default Value)
    '''</param>
    '''<param name="Magnitude">
    '''This control sets the magnitude of the reflection coefficient.
    '''
    '''Valid Range:
    '''
    '''NRP-Z21 0.0 - 1.0
    '''FSH-Z1: 0.0 - 1.0
    '''
    '''Default Value: 1.0
    '''
    '''Notes:
    '''(1) Actual valid range depends on sensor used
    '''</param>
    '''<param name="Phase">
    '''This control defines the phase angle of the reflection coefficient. Units are degrees.
    '''
    '''Valid Range:
    '''-360.0 to 360.0 deg
    '''
    '''Default Value:
    '''0.0 deg
    '''
    '''Notes:
    '''(1) Actual valid range depends on sensor used
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function chan_configureSourceGammaCorr(ByVal Channel As Integer, ByVal Source_Gamma_Correction As Boolean, ByVal Magnitude As Double, ByVal Phase As Double) As Integer
        Dim pInvokeResult As Integer = PInvoke.chan_configureSourceGammaCorr(Me._handle, Channel, System.Convert.ToUInt16(Source_Gamma_Correction), Magnitude, Phase)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function sets the magnitude of the reflection coefficient for measured-value correction.
    '''
    '''Remote-control command(s):
    '''SENS:SGAM:MAGN
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Magnitude">
    '''This control sets the magnitude of the reflection coefficient.
    '''
    '''Valid Range:
    '''
    '''NRP-Z21 0.0 - 1.0
    '''FSH-Z1: 0.0 - 1.0
    '''
    '''Default Value: 1.0
    '''
    '''Notes:
    '''(1) Actual valid range depends on sensor used
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function chan_setSourceGammaMagnitude(ByVal Channel As Integer, ByVal Magnitude As Double) As Integer
        Dim pInvokeResult As Integer = PInvoke.chan_setSourceGammaMagnitude(Me._handle, Channel, Magnitude)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function reads the magnitude of the reflection coefficient for measured-value correction.
    '''
    '''Remote-control command(s):
    '''SENS:SGAM:MAGN?
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Magnitude">
    '''This control returns the magnitude of the reflection coefficient for measured-value correction.
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function chan_getSourceGammaMagnitude(ByVal Channel As Integer, ByRef Magnitude As Double) As Integer
        Dim pInvokeResult As Integer = PInvoke.chan_getSourceGammaMagnitude(Me._handle, Channel, Magnitude)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function sets the phase angle of the reflection coefficient for measured-value correction.
    '''
    '''Remote-control command(s):
    '''SENS:SGAM:PHAS
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Phase">
    '''This control defines the phase angle of the reflection coefficient. Units are degrees.
    '''
    '''Valid Range:
    '''-360.0 to 360.0 deg
    '''
    '''Default Value:
    '''0.0 deg
    '''
    '''Notes:
    '''(1) Actual valid range depends on sensor used
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function chan_setSourceGammaPhase(ByVal Channel As Integer, ByVal Phase As Double) As Integer
        Dim pInvokeResult As Integer = PInvoke.chan_setSourceGammaPhase(Me._handle, Channel, Phase)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function reads the phase angle of the reflection coefficient for measured-value correction.
    '''
    '''Remote-control command(s):
    '''SENS:SGAM:PHAS?
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Phase">
    '''This control returns the phase angle of the reflection coefficient. Units are degrees.
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function chan_getSourceGammaPhase(ByVal Channel As Integer, ByRef Phase As Double) As Integer
        Dim pInvokeResult As Integer = PInvoke.chan_getSourceGammaPhase(Me._handle, Channel, Phase)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function switches the measured-value correction of the reflection coefficient effect of the source gamma ON or OFF.
    '''
    '''Remote-control command(s):
    '''SENS:SGAM:CORR:STAT ON | OFF
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Source_Gamma_Correction">
    '''This control enables or disables source gamma correction of the measured value.
    '''
    '''Valid Values:
    '''VI_TRUE  (1) - On
    '''VI_FALSE (0) - Off (Default Value)
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function chan_setSourceGammaCorrEnabled(ByVal Channel As Integer, ByVal Source_Gamma_Correction As Boolean) As Integer
        Dim pInvokeResult As Integer = PInvoke.chan_setSourceGammaCorrEnabled(Me._handle, Channel, System.Convert.ToUInt16(Source_Gamma_Correction))
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function reads the state of source gamma correction.
    '''
    '''Remote-control command(s):
    '''SENS:RGAM:CORR:STAT?
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Source_Gamma_Correction">
    '''This control returns the state of source gamma correction.
    '''
    '''Valid Values:
    '''VI_TRUE  (1) - On
    '''VI_FALSE (0) - Off
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function chan_getSourceGammaCorrEnabled(ByVal Channel As Integer, ByRef Source_Gamma_Correction As Boolean) As Integer
        Dim Source_Gamma_CorrectionAsUShort As UShort
        Dim pInvokeResult As Integer = PInvoke.chan_getSourceGammaCorrEnabled(Me._handle, Channel, Source_Gamma_CorrectionAsUShort)
        Source_Gamma_Correction = System.Convert.ToBoolean(Source_Gamma_CorrectionAsUShort)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function sets the parameters of the compensation of the load distortion at the signal output.
    '''
    '''Note(s):
    '''
    '''(1) This function is available only for sensors NRP-Z27 and NRP-Z37
    '''
    '''Remote-control command(s):
    '''SENS:RGAM
    '''SENS:RGAM:PHAS
    '''
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Magnitude">
    '''This control sets the magnitude of the reflection coefficient of the load for distortion compensation.
    '''
    '''Valid Range:
    '''0.0 - 1.0
    '''
    '''Default Value: 0.0
    '''</param>
    '''<param name="Phase">
    '''This control defines the phase angle (in degrees) of the complex reflection factor of the load at the signal output.
    '''
    '''Valid Range:
    '''-360.0 to 360.0 deg
    '''
    '''Default Value:
    '''0.0 deg
    '''
    '''Notes:
    '''(1) Actual valid range depends on sensor used
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function chan_configureReflectGammaCorr(ByVal Channel As Integer, ByVal Magnitude As Double, ByVal Phase As Double) As Integer
        Dim pInvokeResult As Integer = PInvoke.chan_configureReflectGammaCorr(Me._handle, Channel, Magnitude, Phase)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function sets the magnitude of the reflection coefficient of the load for distortion compensation.
    '''To deactivate distortion compensation, set Magnitude to 0. Distortion compensation should remain deactivated in the case of questionable measurement values for the reflection coefficient of the load.
    '''
    '''Note(s):
    '''
    '''(1) This function is available only for sensors NRP-Z27 and NRP-Z37
    '''
    '''Remote-control command(s):
    '''SENS:RGAM:MAGN
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Magnitude">
    '''This control sets the magnitude of the reflection coefficient of the load for distortion compensation.
    '''
    '''Valid Range:
    '''0.0 - 1.0
    '''
    '''Default Value: 0.0
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function chan_setReflectionGammaMagn(ByVal Channel As Integer, ByVal Magnitude As Double) As Integer
        Dim pInvokeResult As Integer = PInvoke.chan_setReflectionGammaMagn(Me._handle, Channel, Magnitude)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function reads the magnitude of the reflection coefficient of the load for distortion compensation.
    '''
    '''Note(s):
    '''
    '''(1) This function is available only for sensors NRP-Z27 and NRP-Z37
    '''
    '''Remote-control command(s):
    '''SENS:RGAM:MAGN?
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Magnitude">
    '''This control returns the magnitude of the reflection coefficient of the load for distortion compensation.
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function chan_getReflectionGammaMagn(ByVal Channel As Integer, ByRef Magnitude As Double) As Integer
        Dim pInvokeResult As Integer = PInvoke.chan_getReflectionGammaMagn(Me._handle, Channel, Magnitude)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function defines the phase angle (in degrees) of the complex reflection factor of the load at the signal output.
    '''
    '''Note(s):
    '''
    '''(1) This function is available only for sensors NRP-Z27 and NRP-Z37
    '''
    '''Remote-control command(s):
    '''SENS:RGAM:PHAS
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Phase">
    '''This control defines the phase angle (in degrees) of the complex reflection factor of the load at the signal output.
    '''
    '''Valid Range:
    '''-360.0 to 360.0 deg
    '''
    '''Default Value:
    '''0.0 deg
    '''
    '''Notes:
    '''(1) Actual valid range depends on sensor used
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function chan_setReflectionGammaPhase(ByVal Channel As Integer, ByVal Phase As Double) As Integer
        Dim pInvokeResult As Integer = PInvoke.chan_setReflectionGammaPhase(Me._handle, Channel, Phase)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function reads the phase angle (in degrees) of the complex reflection factor of the load at the signal output.
    '''
    '''Note(s):
    '''
    '''(1) This function is available only for sensors NRP-Z27 and NRP-Z37
    '''
    '''Remote-control command(s):
    '''SENS:RGAM:PHAS?
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Phase">
    '''This control returns the phase angle (in degrees) of the complex reflection factor of the load at the signal output.
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function chan_getReflectionGammaPhase(ByVal Channel As Integer, ByRef Phase As Double) As Integer
        Dim pInvokeResult As Integer = PInvoke.chan_getReflectionGammaPhase(Me._handle, Channel, Phase)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function defines reflection gamma uncertainty.
    '''
    '''Note(s):
    '''
    '''(1) This function is available only for sensors NRP-Z27 and NRP-Z37
    '''
    '''Remote-control command(s):
    '''SENS:RGAM:EUNC
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Uncertainty">
    '''This control defines the uncertainty.
    '''
    '''Valid Range:
    '''0.0 to 1.0
    '''
    '''Default Value:
    '''0.0
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function chan_setReflectionGammaUncertainty(ByVal Channel As Integer, ByVal Uncertainty As Double) As Integer
        Dim pInvokeResult As Integer = PInvoke.chan_setReflectionGammaUncertainty(Me._handle, Channel, Uncertainty)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function queries reflection gamma uncertainty.
    '''
    '''Note(s):
    '''
    '''(1) This function is available only for sensors NRP-Z27 and NRP-Z37
    '''
    '''Remote-control command(s):
    '''SENS:RGAM:EUNC?
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Uncertainty">
    '''This control returns the uncertainty.
    '''
    '''Valid Range:
    '''0.0 to 1.0
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function chan_getReflectionGammaUncertainty(ByVal Channel As Integer, ByRef Uncertainty As Double) As Integer
        Dim pInvokeResult As Integer = PInvoke.chan_getReflectionGammaUncertainty(Me._handle, Channel, Uncertainty)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function configures all duty cycle parameters.
    '''
    '''Remote-control command(s):
    '''SENS:CORR:DCYC
    '''SENS:CORR:DCYC:STAT ON | OFF
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Duty_Cycle_State">
    '''This control switches measured-value correction for a specific duty cycle on or off.
    '''
    '''Valid Values:
    '''VI_TRUE  (1) - On
    '''VI_FALSE (0) - Off (Default Value)
    '''</param>
    '''<param name="Duty_Cycle">
    '''This control sets the duty cycle of power to be measured.
    '''
    '''Valid Range:
    '''0.001 - 99.999%
    '''
    '''Default Value: 1.0 %
    '''
    '''Notes:
    '''(1) Actual valid range depends on sensor used
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function corr_configureDutyCycle(ByVal Channel As Integer, ByVal Duty_Cycle_State As Boolean, ByVal Duty_Cycle As Double) As Integer
        Dim pInvokeResult As Integer = PInvoke.corr_configureDutyCycle(Me._handle, Channel, System.Convert.ToUInt16(Duty_Cycle_State), Duty_Cycle)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function informs the R&amp;S NRP about the duty cycle of the power to be measured. Specifying a duty cycle only makes sense in the ContAv mode where measurements are performed continuously without taking the timing of the signal into account. For this reason, this setting can only be chosen in the local mode when the sensor performs measurements in the ContAv mode.
    '''
    '''Remote-control command(s):
    '''SENS:CORR:DCYC
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Duty_Cycle">
    '''This control sets the duty cycle of power to be measured.
    '''
    '''Valid Range:
    '''0.001 - 99.999 %
    '''
    '''Default Value: 1.0 %
    '''
    '''Notes:
    '''(1) Actual valid range depends on sensor used
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function corr_setDutyCycle(ByVal Channel As Integer, ByVal Duty_Cycle As Double) As Integer
        Dim pInvokeResult As Integer = PInvoke.corr_setDutyCycle(Me._handle, Channel, Duty_Cycle)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function gets the size of duty cycle of the power to be measured.
    '''
    '''Remote-control command(s):
    '''SENS:CORR:DCYC?
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Duty_Cycle">
    '''This control returns the size of duty cycle of the power to be measured. Units are %.
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function corr_getDutyCycle(ByVal Channel As Integer, ByRef Duty_Cycle As Double) As Integer
        Dim pInvokeResult As Integer = PInvoke.corr_getDutyCycle(Me._handle, Channel, Duty_Cycle)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function switches measured-value correction for a specific duty cycle on or off.
    '''
    '''Remote-control command(s):
    '''SENS:CORR:DCYC:STAT ON | OFF
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Duty_Cycle_State">
    '''This control switches measured-value correction for a specific duty cycle on or off.
    '''
    '''Valid Values:
    '''VI_TRUE  (1) - On
    '''VI_FALSE (0) - Off (Default Value)
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function corr_setDutyCycleEnabled(ByVal Channel As Integer, ByVal Duty_Cycle_State As Boolean) As Integer
        Dim pInvokeResult As Integer = PInvoke.corr_setDutyCycleEnabled(Me._handle, Channel, System.Convert.ToUInt16(Duty_Cycle_State))
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function gets the setting of duty cycle.
    '''
    '''Remote-control command(s):
    '''SENS:CORR:DCYC:STAT?
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Duty_Cycle_State">
    '''This control returns the state of duty cycle.
    '''
    '''Valid Values:
    '''VI_TRUE  (1) - On
    '''VI_FALSE (0) - Off
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function corr_getDutyCycleEnabled(ByVal Channel As Integer, ByRef Duty_Cycle_State As Boolean) As Integer
        Dim Duty_Cycle_StateAsUShort As UShort
        Dim pInvokeResult As Integer = PInvoke.corr_getDutyCycleEnabled(Me._handle, Channel, Duty_Cycle_StateAsUShort)
        Duty_Cycle_State = System.Convert.ToBoolean(Duty_Cycle_StateAsUShort)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function determines the integration time for a single measurement in the ContAv mode. To increase the measurement accuracy, this integration is followed by a second averaging procedure in a window with a selectable number of values.
    '''
    '''Remote-control command(s):
    '''SENS:POW:AVG:APER
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="ContAv_Aperture">
    '''This control defines the ContAv Aperture in seconds.
    '''
    '''Valid Range:
    '''NRP-Z21:   0.1e-6 to 0.3 seconds
    '''NRP-Z51:   0.1e-3 to 0.3 seconds
    '''FSH-Z1:    0.1e-6 to 0.3 seconds
    '''
    '''Default Value: 0.02 s
    '''
    '''Notes:
    '''(1) Actual valid range depends on sensor used
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function chan_setContAvAperture(ByVal Channel As Integer, ByVal ContAv_Aperture As Double) As Integer
        Dim pInvokeResult As Integer = PInvoke.chan_setContAvAperture(Me._handle, Channel, ContAv_Aperture)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function queries the value of ContAv mode aperture size.
    '''
    '''Remote-control command(s):
    '''SENS:POW:AVG:APER?
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="ContAv_Aperture">
    '''This control returns the ContAv Aperture size in seconds.
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function chan_getContAvAperture(ByVal Channel As Integer, ByRef ContAv_Aperture As Double) As Integer
        Dim pInvokeResult As Integer = PInvoke.chan_getContAvAperture(Me._handle, Channel, ContAv_Aperture)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function activates digital lowpass filtering of the sampled video signal.
    '''The problem of instable display values due to a modulation of a test signal can also be eliminated by lowpass filtering of the video signal. The lowpass filter eliminates the variations of the display even in case of unperiodic modulation and does not require any other setting.
    '''If the modulation is periodic, the setting of the sampling window is the better method since it allows for shorter measurement times.
    '''
    '''Remote-control command(s):
    '''SENS:POW:AVG:SMO:STAT ON | OFF
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="ContAv_Smoothing">
    '''This control sets the state of digital lowpass filtering of the sampled video signal.
    '''
    '''Valid Values:
    '''VI_TRUE  (1) - On (Default Value)
    '''VI_FALSE (0) - Off
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function chan_setContAvSmoothingEnabled(ByVal Channel As Integer, ByVal ContAv_Smoothing As Boolean) As Integer
        Dim pInvokeResult As Integer = PInvoke.chan_setContAvSmoothingEnabled(Me._handle, Channel, System.Convert.ToUInt16(ContAv_Smoothing))
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function gets the state of digital lowpass filtering of the sampled video signal.
    '''
    '''Remote-control command(s):
    '''SENS:POW:AVG:SMO:STAT?
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="ContAv_Smoothing">
    '''This control returns the state of digital lowpass filtering of the sampled video signal.
    '''
    '''Valid Values:
    '''VI_TRUE  (1) - On
    '''VI_FALSE (0) - Off
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function chan_getContAvSmoothingEnabled(ByVal Channel As Integer, ByRef ContAv_Smoothing As Boolean) As Integer
        Dim ContAv_SmoothingAsUShort As UShort
        Dim pInvokeResult As Integer = PInvoke.chan_getContAvSmoothingEnabled(Me._handle, Channel, ContAv_SmoothingAsUShort)
        ContAv_Smoothing = System.Convert.ToBoolean(ContAv_SmoothingAsUShort)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function switches on the buffered ContAv mode, after which data blocks rather than single measured values are then  returned. In this mode a higher data rate is achieved than in the non-buffered ContAv mode.
    '''
    '''Remote-control command(s):
    '''SENS:POW:AVG:BUFF:STAT ON | OFF
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="ContAv_Buffered_Mode">
    '''This control turns on or off ContAv buffered measurement mode.
    '''
    '''Valid Values:
    '''VI_TRUE  (1) - On
    '''VI_FALSE (0) - Off (Default Value)
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function chan_setContAvBufferedEnabled(ByVal Channel As Integer, ByVal ContAv_Buffered_Mode As Boolean) As Integer
        Dim pInvokeResult As Integer = PInvoke.chan_setContAvBufferedEnabled(Me._handle, Channel, System.Convert.ToUInt16(ContAv_Buffered_Mode))
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function returns the state of ContAv Buffered Measurement Mode.
    '''
    '''Remote-control command(s):
    '''SENS:POW:AVG:BUFF:STAT?
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="ContAv_Buffered_Mode">
    '''This control returns the state of ContAv Buffered Measurement Mode.
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function chan_getContAvBufferedEnabled(ByVal Channel As Integer, ByRef ContAv_Buffered_Mode As Boolean) As Integer
        Dim ContAv_Buffered_ModeAsUShort As UShort
        Dim pInvokeResult As Integer = PInvoke.chan_getContAvBufferedEnabled(Me._handle, Channel, ContAv_Buffered_ModeAsUShort)
        ContAv_Buffered_Mode = System.Convert.ToBoolean(ContAv_Buffered_ModeAsUShort)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function sets the number of desired values for the buffered ContAv mode.
    '''
    '''Remote-control command(s):
    '''SENS:POW:AVG:BUFF:SIZE
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Buffer_Size">
    '''This control sets the number of desired values for buffered ContAv mode.
    '''
    '''Valid Range:
    '''1 to 1024
    '''
    '''Default Value: 1
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function chan_setContAvBufferSize(ByVal Channel As Integer, ByVal Buffer_Size As Integer) As Integer
        Dim pInvokeResult As Integer = PInvoke.chan_setContAvBufferSize(Me._handle, Channel, Buffer_Size)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function returns the number of desired values for the buffered ContAv mode.
    '''
    '''Remote-control command(s):
    '''SENS:POW:AVG:BUFF:SIZE?
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Buffer_Size">
    '''This control returns the number of desired values for the buffered ContAv mode.
    '''
    '''Valid Range:
    '''1 to 400000
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function chan_getContAvBufferSize(ByVal Channel As Integer, ByRef Buffer_Size As Integer) As Integer
        Dim pInvokeResult As Integer = PInvoke.chan_getContAvBufferSize(Me._handle, Channel, Buffer_Size)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function returns the number of measurement values currently stored in the sensor buffer while the buffered measurement is running.
    '''
    '''Remote-control command(s):
    '''SENS:POW:AVG:BUFF:COUN?
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Buffer_Count">
    '''This control returns the number of measurement values currently stored in the sensor buffer while the buffered measurement is running.
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function chan_getContAvBufferCount(ByVal Channel As Integer, ByRef Buffer_Count As Integer) As Integer
        Dim pInvokeResult As Integer = PInvoke.chan_getContAvBufferCount(Me._handle, Channel, Buffer_Count)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function returns some important settings for the scalar network analysis.
    '''
    '''Remote-control command(s):
    '''SENS:POW:AVG:BUFF:INFO? &lt;Info Type&gt;
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Info_Type">
    '''This control specifies which info should be retrieved from the sensor. If no infoType is given the sensor returns the complete information string.
    '''
    '''Valid Values:
    ''' "FAST"
    ''' "NORMAL"
    ''' "HIGHPRECISION"
    ''' "TRACEMODE"
    '''
    '''Default Value:
    '''""
    '''</param>
    '''<param name="Array_Size">
    '''This control defines the size of array 'Info'.
    '''
    '''Valid Range:
    '''-
    '''
    '''Default Value:
    '''100
    '''</param>
    '''<param name="Info">
    '''This control returns some important settings for the scalar network analysis.
    '''
    '''The information for the types "FAST", "NORMAL" and "HIGHPRECISION" is a comma separated string including the following fields:
    '''
    '''1.) infotype,
    '''2.) aperture time
    '''3.) average count
    '''4.) min. time between two trigger events 
    '''5.) trigger delay
    '''6.) flag if this mode is available (0 if not)
    '''
    '''The type "TRACEMODE" returns a "1" if tracemode is supported by the sensor.
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function chan_getContAvBufferInfo(ByVal Channel As Integer, ByVal Info_Type As String, ByVal Array_Size As Integer, ByVal Info As System.Text.StringBuilder) As Integer
        Dim pInvokeResult As Integer = PInvoke.chan_getContAvBufferInfo(Me._handle, Channel, Info_Type, Array_Size, Info)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''The end of a burst (power pulse) is recognized when the signal level drops below the trigger level. Especially with modulated signals, this may also happen for a short time within a burst. To prevent the supposed end of the burst is from being recognized too early or incorrectly at these positions, a time interval can be defined via using this function (drop-out tolerance parameter) in which the pulse end is only recognized if the signal level no longer exceeds the trigger level.
    '''
    '''Note:
    '''
    '''1) This function is not available for NRP-Z51
    '''
    '''Remote-control command(s):
    '''SENS:POW:BURS:DTOL
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Drop_out_Tolerance">
    '''This parameter defines the Drop-Out Tolerance time interval in seconds.
    '''
    '''Valid Range:
    '''0.0 to 3.0e-3 seconds
    '''
    '''Default Value: 100.0e-6 s
    '''
    '''Notes:
    '''(1) Actual valid range depends on sensor used
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''
    '''</returns>
    Public Function chan_setBurstDropoutTolerance(ByVal Channel As Integer, ByVal Drop_out_Tolerance As Double) As Integer
        Dim pInvokeResult As Integer = PInvoke.chan_setBurstDropoutTolerance(Me._handle, Channel, Drop_out_Tolerance)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function returns the drop-out tolerance parameter.
    '''
    '''Note:
    '''
    '''1) This function is not available for NRP-Z51
    '''
    '''Remote-control command(s):
    '''SENS:POW:BURS:DTOL?
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Drop_out_Tolerance">
    '''This control returns the drop-out tolerance parameter.
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function chan_getBurstDropoutTolerance(ByVal Channel As Integer, ByRef Drop_out_Tolerance As Double) As Integer
        Dim pInvokeResult As Integer = PInvoke.chan_getBurstDropoutTolerance(Me._handle, Channel, Drop_out_Tolerance)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function enables or disables the chopper in BurstAv mode. Disabling the chopper is only good for fast but unexact and noisy measurements. If the chopper is disabled, averaging is ignored internally also disabled.
    '''
    '''Remote-control command(s):
    '''SENSe:POWer:BURSt:CHOPper:STATe
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="BurstAv_Chopper">
    '''This control enables or disables the chopper for BurstAv mode.
    '''
    '''Valid Values:
    '''VI_TRUE  (1) - On (Default Value)
    '''VI_FALSE (0) - Off
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function chan_setBurstChopperEnabled(ByVal Channel As Integer, ByVal BurstAv_Chopper As Boolean) As Integer
        Dim pInvokeResult As Integer = PInvoke.chan_setBurstChopperEnabled(Me._handle, Channel, System.Convert.ToUInt16(BurstAv_Chopper))
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function queries the state of the chopper in BurstAv mode.
    '''
    '''Remote-control command(s):
    '''SENSe:POWer:BURSt:CHOPper:STATe?
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="BurstAv_Chopper">
    '''This control returns the state of the chopper for BurstAv mode.
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function chan_getBurstChopperEnabled(ByVal Channel As Integer, ByRef BurstAv_Chopper As Boolean) As Integer
        Dim BurstAv_ChopperAsUShort As UShort
        Dim pInvokeResult As Integer = PInvoke.chan_getBurstChopperEnabled(Me._handle, Channel, BurstAv_ChopperAsUShort)
        BurstAv_Chopper = System.Convert.ToBoolean(BurstAv_ChopperAsUShort)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function configures the timegate (depends on trigger event) in which the sensor is doing statistic measurements.
    '''
    '''Remote-control command(s):
    '''SENSe:STATistics:OFFSet:TIME
    '''SENSe:STATistics:TIME
    '''SENSe:STATistics:[EXCLude]:MID:OFFSet[:TIME]
    '''SENSe:STATistics:[EXCLude]:MID:TIME
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Offset">
    '''This control sets the start after the trigger of the timegate in which the sensor is doing statistic measurements.
    '''
    '''Valid Range:
    '''
    '''
    '''Default Value: 0.0 s
    '''</param>
    '''<param name="Time">
    '''This control sets the length of the timegate in which the sensor is doing statistic measurements.
    '''
    '''Valid Range:
    '''1.0E-6 to 0.3 s
    '''
    '''Default Value: 0.01 s
    '''</param>
    '''<param name="Midamble_Offset">
    '''This control sets the midamble offset after timeslot start in seconds in the timegate in which the sensor is doing statistic measurements.
    '''
    '''Valid Range:
    '''0.0 to 10.0 s
    '''
    '''Default Value: 0.0 s
    '''</param>
    '''<param name="Midamble_Length">
    '''This control sets the midamble length in seconds.
    '''
    '''Valid Range:
    '''0.0 to 10.0 s
    '''
    '''Default Value: 0.0 s
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function stat_confTimegate(ByVal Channel As Integer, ByVal Offset As Double, ByVal Time As Double, ByVal Midamble_Offset As Double, ByVal Midamble_Length As Double) As Integer
        Dim pInvokeResult As Integer = PInvoke.stat_confTimegate(Me._handle, Channel, Offset, Time, Midamble_Offset, Midamble_Length)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function sets the X-Axis of statistical measurement.
    '''
    '''Remote-control command(s):
    '''SENSe:STATistics:SCALE:X:RLEVel
    '''SENSe:STATistics:SCALE:X:RANGe
    '''SENSe:STATistics:SCALE:X:POINts
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Reference_Level">
    '''This control sets the lower limit of the level range for the analysis result in both Statistics modes. This level can be assigned to the first pixel. The level assigned to the last pixel is equal to the level of the first pixel plus the level range.
    '''
    '''Valid Range:
    '''-80.0 to 20.0 dB
    '''
    '''Default Value: -30.0 dB
    '''</param>
    '''<param name="Range">
    '''This control sets the width of the level range for the analysis result in both Statistics modes.
    '''
    '''Valid Range:
    '''0.01 to 100.0
    '''
    '''Default Value: 50.0
    '''</param>
    '''<param name="Points">
    '''This control sets the measurement-result resolution in both Statistics modes. This function specifies the number of pixels that are to be assigned to the logarithmic level range (rsnrpz_stat_setScaleRange function) for measured value output. The width of the level range divided by N-1, where N is the number of pixels, must not be less than the value which can be read out with rsnrpz_stat_getScaleWidth.
    '''
    '''Valid Range:
    '''3 to 8192
    '''
    '''Default Value: 200
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function stat_confScale(ByVal Channel As Integer, ByVal Reference_Level As Double, ByVal Range As Double, ByVal Points As Integer) As Integer
        Dim pInvokeResult As Integer = PInvoke.stat_confScale(Me._handle, Channel, Reference_Level, Range, Points)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function sets the start after the trigger of the timegate in which the sensor is doing statistic measurements.
    '''
    '''Remote-control command(s):
    '''SENSe:STATistics:OFFSet:TIME
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Offset">
    '''This control sets the start after the trigger of the timegate in which the sensor is doing statistic measurements.
    '''
    '''Valid Range:
    '''
    '''
    '''Default Value: 0.0 s
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function stat_setOffsetTime(ByVal Channel As Integer, ByVal Offset As Double) As Integer
        Dim pInvokeResult As Integer = PInvoke.stat_setOffsetTime(Me._handle, Channel, Offset)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function queries the start after the trigger of the timegate in which the sensor is doing statistic measurements.
    '''
    '''Remote-control command(s):
    '''SENSe:STATistics:OFFSet:TIME?
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Offset">
    '''This control returns the start after the trigger of the timegate in which the sensor is doing statistic measurements.
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function stat_getOffsetTime(ByVal Channel As Integer, ByRef Offset As Double) As Integer
        Dim pInvokeResult As Integer = PInvoke.stat_getOffsetTime(Me._handle, Channel, Offset)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function sets the length of the timegate in which the sensor is doing statistic measurements.
    '''
    '''Remote-control command(s):
    '''SENSe:STATistics:TIME
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Time">
    '''This control sets the length of the timegate in which the sensor is doing statistic measurements.
    '''
    '''Valid Range:
    '''1.0E-6 to 0.3 s
    '''
    '''Default Value: 0.01 s
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function stat_setTime(ByVal Channel As Integer, ByVal Time As Double) As Integer
        Dim pInvokeResult As Integer = PInvoke.stat_setTime(Me._handle, Channel, Time)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function queries the length of the timegate in which the sensor is doing statistic measurements.
    '''
    '''Remote-control command(s):
    '''SENSe:STATistics:TIME?
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Time">
    '''This control returns the length of the timegate in which the sensor is doing statistic measurements.
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function stat_getTime(ByVal Channel As Integer, ByRef Time As Double) As Integer
        Dim pInvokeResult As Integer = PInvoke.stat_getTime(Me._handle, Channel, Time)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function sets the midamble offset after timeslot start in seconds in the timegate in which the sensor is doing statistic measurements.
    '''
    '''Remote-control command(s):
    '''SENSe:STATistics:[EXCLude]:MID:OFFSet[:TIME]
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Offset">
    '''This control sets the midamble offset after timeslot start in seconds in the timegate in which the sensor is doing statistic measurements.
    '''
    '''Valid Range:
    '''0.0 to 10.0 s
    '''
    '''Default Value: 0.0 s
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function stat_setMidOffset(ByVal Channel As Integer, ByVal Offset As Double) As Integer
        Dim pInvokeResult As Integer = PInvoke.stat_setMidOffset(Me._handle, Channel, Offset)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function queries the midamble offset after timeslot start in seconds in the timegate in which the sensor is doing statistic measurements.
    '''
    '''Remote-control command(s):
    '''SENSe:STATistics:[EXCLude]:MID:OFFSet[:TIME]?
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Offset">
    '''This control returns the midamble offset after timeslot start in seconds in the timegate in which the sensor is doing statistic measurements.
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function stat_getMidOffset(ByVal Channel As Integer, ByRef Offset As Double) As Integer
        Dim pInvokeResult As Integer = PInvoke.stat_getMidOffset(Me._handle, Channel, Offset)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function sets the midamble length in seconds.
    '''
    '''Remote-control command(s):
    '''SENSe:STATistics:[EXCLude]:MID:TIME
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Length">
    '''This control sets the midamble length in seconds.
    '''
    '''Valid Range:
    '''0.0 to 10.0 s
    '''
    '''Default Value: 0.0 s
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function stat_setMidLength(ByVal Channel As Integer, ByVal Length As Double) As Integer
        Dim pInvokeResult As Integer = PInvoke.stat_setMidLength(Me._handle, Channel, Length)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function queries the midamble length in seconds.
    '''
    '''Remote-control command(s):
    '''SENSe:STATistics:[EXCLude]:MID:TIME?
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Length">
    '''This control returns the midamble length in seconds.
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function stat_getMidLength(ByVal Channel As Integer, ByRef Length As Double) As Integer
        Dim pInvokeResult As Integer = PInvoke.stat_getMidLength(Me._handle, Channel, Length)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function sets the lower limit of the level range for the analysis result in both Statistics modes. This level can be assigned to the first pixel. The level assigned to the last pixel is equal to the level of the first pixel plus the level range.
    '''
    '''Remote-control command(s):
    '''SENSe:STATistics:SCALE:X:RLEVel
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Reference_Level">
    '''This control sets the lower limit of the level range for the analysis result in both Statistics modes. This level can be assigned to the first pixel. The level assigned to the last pixel is equal to the level of the first pixel plus the level range.
    '''
    '''Valid Range:
    '''-80.0 to 20.0 dB
    '''
    '''Default Value: -30.0 dB
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function stat_setScaleRefLevel(ByVal Channel As Integer, ByVal Reference_Level As Double) As Integer
        Dim pInvokeResult As Integer = PInvoke.stat_setScaleRefLevel(Me._handle, Channel, Reference_Level)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function queries the lower limit of the level range for the analysis result in both Statistics modes. This level can be assigned to the first pixel. The level assigned to the last pixel is equal to the level of the first pixel plus the level range.
    '''
    '''Remote-control command(s):
    '''SENSe:STATistics:SCALE:X:RLEVel?
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Reference_Level">
    '''This control returns the lower limit of the level range for the analysis result in both Statistics modes. This level can be assigned to the first pixel. The level assigned to the last pixel is equal to the level of the first pixel plus the level range.
    '''
    '''Valid Range:
    '''-80.0 to 20.0 dBm
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function stat_getScaleRefLevel(ByVal Channel As Integer, ByRef Reference_Level As Double) As Integer
        Dim pInvokeResult As Integer = PInvoke.stat_getScaleRefLevel(Me._handle, Channel, Reference_Level)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function sets the width of the level range for the analysis result in both Statistics modes.
    '''
    '''Remote-control command(s):
    '''SENSe:STATistics:SCALE:X:RANGe
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Range">
    '''This control sets the width of the level range for the analysis result in both Statistics modes.
    '''
    '''Valid Range:
    '''0.01 to 100.0
    '''
    '''Default Value: 50.0
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function stat_setScaleRange(ByVal Channel As Integer, ByVal Range As Double) As Integer
        Dim pInvokeResult As Integer = PInvoke.stat_setScaleRange(Me._handle, Channel, Range)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function queries the width of the level range for the analysis result in both Statistics modes.
    '''
    '''Remote-control command(s):
    '''SENSe:STATistics:SCALE:X:RANGe?
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Range">
    '''This control returns the width of the level range for the analysis result in both Statistics modes.
    '''
    '''Valid Range:
    '''0.01 to 100
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function stat_getScaleRange(ByVal Channel As Integer, ByRef Range As Double) As Integer
        Dim pInvokeResult As Integer = PInvoke.stat_getScaleRange(Me._handle, Channel, Range)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function sets the measurement-result resolution in both Statistics modes. This function specifies the number of pixels that are to be assigned to the logarithmic level range (rsnrpz_stat_setScaleRange function) for measured value output. The width of the level range divided by N-1, where N is the number of pixels, must not be less than the value which can be read out with rsnrpz_stat_getScaleWidth.
    '''
    '''Remote-control command(s):
    '''SENSe:STATistics:SCALE:X:POINts
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Points">
    '''This control the measurement-result resolution in both Statistics modes.
    '''
    '''Valid Range:
    '''3 to 8192
    '''
    '''Default Value: 200
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function stat_setScalePoints(ByVal Channel As Integer, ByVal Points As Integer) As Integer
        Dim pInvokeResult As Integer = PInvoke.stat_setScalePoints(Me._handle, Channel, Points)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function queries the measurement-result resolution in both Statistics modes.
    '''
    '''Remote-control command(s):
    '''SENSe:STATistics:SCALE:X:POINts?
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Points">
    '''This control returns the measurement-result resolution in both Statistics modes.
    '''
    '''Valid Range:
    '''3 to 8192
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function stat_getScalePoints(ByVal Channel As Integer, ByRef Points As Integer) As Integer
        Dim pInvokeResult As Integer = PInvoke.stat_getScalePoints(Me._handle, Channel, Points)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function queries the greatest attainable level resolution. For the R&amp;S NRP-Z81 power sensor, this value is fixed at 0.006 dB per pixel. If this value is exceeded, a "Settings conflict" message is output. The reason for the conflict may be that the number of pixels that has been selected is too great or that the width chosen for the level range is too small (rsnrpz_stat_setScalePoints and rsnrpz_stat_setScaleRange functions).
    '''
    '''Remote-control command(s):
    '''SENSe:STATistics:SCALE:X:MPWidth?
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Width">
    '''This control returns the greatest attainable level resolution.
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function stat_getScaleWidth(ByVal Channel As Integer, ByRef Width As Double) As Integer
        Dim pInvokeResult As Integer = PInvoke.stat_getScaleWidth(Me._handle, Channel, Width)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function configures the parameters of Timeslot measurement mode. Both exclude start and stop are set to 10% of timeslot width each.
    '''
    '''Remote-control command(s):
    '''SENS:POW:TSL:AVG:COUN
    '''SENS:POW:TSL:AVG:WIDT
    '''SENS:TIM:EXCL:STAR
    '''SENS:TIM:EXCL:STOP
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Time_Slot_Count">
    '''This control sets the number of simultaneously measured timeslots in the Timeslot mode.
    '''
    '''Valid Range:
    '''1 - 128
    '''
    '''Default Value:
    '''8
    '''
    '''Notes:
    '''(1) Actual valid range depends on sensor used
    '''</param>
    '''<param name="Width">
    '''This control sets the length in seconds of the timeslot in the Timeslot mode.
    '''
    '''Valid Range:
    '''10.0e-6 - 0.1
    '''
    '''Default Value: 1.0e-3 s
    '''
    '''Notes:
    '''(1) Actual valid range depends on sensor used
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function tslot_configureTimeSlot(ByVal Channel As Integer, ByVal Time_Slot_Count As Integer, ByVal Width As Double) As Integer
        Dim pInvokeResult As Integer = PInvoke.tslot_configureTimeSlot(Me._handle, Channel, Time_Slot_Count, Width)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function sets the number of simultaneously measured timeslots in the Timeslot mode.
    '''
    '''Note:
    '''
    '''1) This function is not available for NRP-Z51.
    '''
    '''Remote-control command(s):
    '''SENS:POW:TSL:AVG:COUN
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Time_Slot_Count">
    '''This control sets the number of simultaneously measured timeslots in the Timeslot mode.
    '''
    '''Valid Range:
    '''1 - 128
    '''
    '''Default Value:
    '''8
    '''
    '''Notes:
    '''(1) Actual valid range depends on sensor used
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function tslot_setTimeSlotCount(ByVal Channel As Integer, ByVal Time_Slot_Count As Integer) As Integer
        Dim pInvokeResult As Integer = PInvoke.tslot_setTimeSlotCount(Me._handle, Channel, Time_Slot_Count)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function reads the number of simultaneously measured timeslots in the Timeslot mode.
    '''
    '''Note:
    '''
    '''1) This function is not available for NRP-Z51.
    '''
    '''Remote-control command(s):
    '''SENS:POW:TSL:AVG:COUN?
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Time_Slot_Count">
    '''This control returns the number of simultaneously measured timeslots in the Timeslot mode.
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function tslot_getTimeSlotCount(ByVal Channel As Integer, ByRef Time_Slot_Count As Integer) As Integer
        Dim pInvokeResult As Integer = PInvoke.tslot_getTimeSlotCount(Me._handle, Channel, Time_Slot_Count)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function sets the length of the timeslot in the Timeslot mode.
    '''
    '''Note:
    '''
    '''1) This function is not available for NRP-Z51.
    '''
    '''Remote-control command(s):
    '''SENS:POW:TSL:AVG:WIDT
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Width">
    '''This control sets the length in seconds of the timeslot in the Timeslot mode.
    '''
    '''Valid Range:
    '''10.0e-6 - 0.1
    '''
    '''Default Value: 1.0e-3 s
    '''
    '''Notes:
    '''(1) Actual valid range depends on sensor used
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function tslot_setTimeSlotWidth(ByVal Channel As Integer, ByVal Width As Double) As Integer
        Dim pInvokeResult As Integer = PInvoke.tslot_setTimeSlotWidth(Me._handle, Channel, Width)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function reads the length of the timeslot in the Timeslot mode.
    '''
    '''Note:
    '''
    '''1) This function is not available for NRP-Z51.
    '''
    '''Remote-control command(s):
    '''SENS:POW:TSL:AVG:WIDT?
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Width">
    '''This control returns the length in seconds of the timeslot in the Timeslot mode.
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function tslot_getTimeSlotWidth(ByVal Channel As Integer, ByRef Width As Double) As Integer
        Dim pInvokeResult As Integer = PInvoke.tslot_getTimeSlotWidth(Me._handle, Channel, Width)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function sets the start of an exclusion interval in a timeslot. In conjunction with the function rsnrpz_tslot_setTimeSlotMidLength, it is possible to exclude, for example, a midamble from the measurement. The start of the timeslot is used as the reference point for defining the start of the exclusion interval and this applies to each of the timeslots
    '''
    '''Remote-control command(s):
    '''SENSe:POWer:TSLot[:AVG]:MID:OFFSet
    '''
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Offset">
    '''This control sets sets the start of an exclusion interval in a timeslot.
    '''
    '''Valid Range:
    '''0.0 to 0.1 s
    '''
    '''Default Value: 0.0 s
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function tslot_setTimeSlotMidOffset(ByVal Channel As Integer, ByVal Offset As Double) As Integer
        Dim pInvokeResult As Integer = PInvoke.tslot_setTimeSlotMidOffset(Me._handle, Channel, Offset)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function queries the start of an exclusion interval in a timeslot.
    '''
    '''Remote-control command(s):
    '''SENSe:POWer:TSLot[:AVG]:MID:OFFSet?
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Offset">
    '''This control returns sets the start of an exclusion interval in a timeslot.
    '''
    '''Valid Range:
    '''0.0 to 0.1 s
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function tslot_getTimeSlotMidOffset(ByVal Channel As Integer, ByRef Offset As Double) As Integer
        Dim pInvokeResult As Integer = PInvoke.tslot_getTimeSlotMidOffset(Me._handle, Channel, Offset)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function sets the length of an exclusion interval in a timeslot. In conjunction with the function rsnrpz_tslot_setTimeSlotMidOffset, it can be used, for example, to exclude a midamble from the measurement.
    '''
    '''Remote-control command(s):
    '''SENSe:POWer:TSLot[:AVG]:MID:TIME
    '''
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Length">
    '''This control sets the length of an exclusion interval in a timeslot.
    '''
    '''Valid Range:
    '''0.0 to 0.1
    '''
    '''Default Value: 0.0 s
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function tslot_setTimeSlotMidLength(ByVal Channel As Integer, ByVal Length As Double) As Integer
        Dim pInvokeResult As Integer = PInvoke.tslot_setTimeSlotMidLength(Me._handle, Channel, Length)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function queries the length of an exclusion interval in a timeslot.
    '''
    '''Remote-control command(s):
    '''SENSe:POWer:TSLot[:AVG]:MID:TIME?
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Length">
    '''This control returns the length of an exclusion interval in a timeslot.
    '''
    '''Valid Range:
    '''0.0 to 0.1 s
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function tslot_getTimeSlotMidLength(ByVal Channel As Integer, ByRef Length As Double) As Integer
        Dim pInvokeResult As Integer = PInvoke.tslot_getTimeSlotMidLength(Me._handle, Channel, Length)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function enables or disables the chopper in Time Slot mode. Disabling the chopper is only good for fast but unexact and noisy measurements. If the chopper is disabled, averaging is ignored internally also disabled.
    '''
    '''Remote-control command(s):
    '''SENSe:POWer:TSLot[:AVG]:CHOPper:STATe
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Time_Slot_Chopper">
    '''This control enables or disables the chopper for Time Slot mode.
    '''
    '''Valid Values:
    '''VI_TRUE  (1) - On (Default Value)
    '''VI_FALSE (0) - Off
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function tslot_setTimeSlotChopperEnabled(ByVal Channel As Integer, ByVal Time_Slot_Chopper As Boolean) As Integer
        Dim pInvokeResult As Integer = PInvoke.tslot_setTimeSlotChopperEnabled(Me._handle, Channel, System.Convert.ToUInt16(Time_Slot_Chopper))
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function queries the state of the chopper in Time Slot mode.
    '''
    '''Remote-control command(s):
    '''SENSe:POWer:TSLot[:AVG]:CHOPper:STATe?
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Time_Slot_Chopper">
    '''This control returns the state of the chopper for Time Slot mode.
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function tslot_getTimeSlotChopperEnabled(ByVal Channel As Integer, ByRef Time_Slot_Chopper As Boolean) As Integer
        Dim Time_Slot_ChopperAsUShort As UShort
        Dim pInvokeResult As Integer = PInvoke.tslot_getTimeSlotChopperEnabled(Me._handle, Channel, Time_Slot_ChopperAsUShort)
        Time_Slot_Chopper = System.Convert.ToBoolean(Time_Slot_ChopperAsUShort)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function sets parameters of the Scope mode.
    '''
    '''Note:
    '''
    '''1) This function is not available for NRP-Z51.
    '''
    '''Remote-control command(s):
    '''SENS:TRAC:POIN
    '''SENS:TRAC:TIME
    '''SENS:TRAC:OFFS:TIME
    '''SENS:TRAC:REAL ON | OFF
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Scope_Points">
    '''This control sets the number of desired values per Scope sequence.
    '''
    '''Valid Range:
    '''1 to 1024
    '''
    '''Default Value: 312
    '''
    '''Notes:
    '''(1) Actual valid range depends on sensor used
    '''</param>
    '''<param name="Scope_Time">
    '''This control sets the value of scope time.
    '''
    '''Valid Range:
    '''0.1e-3 to 0.3 s
    '''
    '''Default Value: 0.01 s
    '''
    '''Notes:
    '''(1) Actual valid range depends on sensor used
    '''</param>
    '''<param name="Offset_Time">
    '''This control sets the value of offset time.
    '''
    '''Valid Range:
    '''
    '''-5.0e-3 to 100.0 s
    '''
    '''Default Value: 0.0 s
    '''
    '''
    '''Notes:
    '''(1) Actual valid range depends on sensor used
    '''</param>
    '''<param name="Realtime">
    '''This control sets the state of real-time measurement.
    '''
    '''Valid Values:
    '''VI_TRUE  (1) - On (Default Value)
    '''VI_FALSE (0) - Off
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function scope_configureScope(ByVal Channel As Integer, ByVal Scope_Points As Integer, ByVal Scope_Time As Double, ByVal Offset_Time As Double, ByVal Realtime As Boolean) As Integer
        Dim pInvokeResult As Integer = PInvoke.scope_configureScope(Me._handle, Channel, Scope_Points, Scope_Time, Offset_Time, System.Convert.ToUInt16(Realtime))
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function performs fast zeroing, but can be called only in the sensor's Trace mode and Statistics modes. In any other measurement mode, the error message NRPERROR_CALZERO is output. Even though the execution time is shorter than that for standard zeroing by a factor of 100 or more, measurement accuracy is not affected. Fast zeroing is available for the entire frequency range.
    '''
    '''Remote-control command(s):
    '''CAL:ZERO:FAST:AUTO
    '''</summary>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function scope_fastZero() As Integer
        Dim pInvokeResult As Integer = PInvoke.scope_fastZero(Me._handle)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''For the Scope mode, this function switches the filter function of a sensor on or off. When the filter is switched on, the number of measured values set with SENS:TRAC:AVER:COUN (function rsnrpz_scope_setAverageCount) is averaged. This reduces the effect of noise so that more reliable results are obtained.
    '''
    '''Note:
    '''
    '''1) This function is not available for NRP-Z51.
    '''
    '''Remote-control command(s):
    '''SENS:TRAC:AVER:STAT ON | OFF
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Scope_Averaging">
    '''This control switches the filter function of a sensor on or off.
    '''
    '''Valid Values:
    '''VI_TRUE  (1) - On (Default Value)
    '''VI_FALSE (0) - Off
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function scope_setAverageEnabled(ByVal Channel As Integer, ByVal Scope_Averaging As Boolean) As Integer
        Dim pInvokeResult As Integer = PInvoke.scope_setAverageEnabled(Me._handle, Channel, System.Convert.ToUInt16(Scope_Averaging))
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function reads the state of filter function of a sensor.
    '''
    '''Note:
    '''
    '''1) This function is not available for NRP-Z51.
    '''
    '''Remote-control command(s):
    '''SENS:TRAC:AVER:STAT?
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Scope_Averaging">
    '''This control returns the state of filter function of a sensor.
    '''
    '''Valid Values:
    '''VI_TRUE  (1) - On
    '''VI_FALSE (0) - Off
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function scope_getAverageEnabled(ByVal Channel As Integer, ByRef Scope_Averaging As Boolean) As Integer
        Dim Scope_AveragingAsUShort As UShort
        Dim pInvokeResult As Integer = PInvoke.scope_getAverageEnabled(Me._handle, Channel, Scope_AveragingAsUShort)
        Scope_Averaging = System.Convert.ToBoolean(Scope_AveragingAsUShort)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function sets the length of the filter for the Scope mode. The wider the filter the lower the noise and the longer it takes to obtain a measured value.
    '''
    '''Remote-control command(s):
    '''SENS:TRAC:AVER:COUN
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Count">
    '''This control sets the length of the filter for the Scope mode.
    '''
    '''Valid Range:
    '''1 to 65536
    '''
    '''Default Value: 4
    '''
    '''Notes:
    '''(1) Actual valid range depends on sensor used
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function scope_setAverageCount(ByVal Channel As Integer, ByVal Count As Integer) As Integer
        Dim pInvokeResult As Integer = PInvoke.scope_setAverageCount(Me._handle, Channel, Count)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function returns the length of the filter for the Scope mode.
    '''
    '''Remote-control command(s):
    '''SENS:TRAC:AVER:COUN?
    '''
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Count">
    '''This control returns the averaging filter length in Scope mode.
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function scope_getAverageCount(ByVal Channel As Integer, ByRef Count As Integer) As Integer
        Dim pInvokeResult As Integer = PInvoke.scope_getAverageCount(Me._handle, Channel, Count)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''As soon as a new single value is determined, the filter window is advanced by one value so that the new value is taken into account by the filter and the oldest value is forgotten.
    '''Terminal control then determines in the Scope mode whether a new result will be calculated immediately after a new measured value is available or only after an entire range of new values is available for the filter.
    '''
    '''Note:
    '''
    '''1) This function is not available for NRP-Z51.
    '''
    '''Remote-control command(s):
    '''SENS:TRAC:AVER:TCON MOV | REP
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Terminal_Control">
    '''This control determines the type of terminal control.
    '''
    '''Valid Values:
    ''' RSNRPZ_TERMINAL_CONTROL_MOVING - Moving
    ''' RSNRPZ_TERMINAL_CONTROL_REPEAT - Repeat (Default Value)
    '''
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function scope_setAverageTerminalControl(ByVal Channel As Integer, ByVal Terminal_Control As Integer) As Integer
        Dim pInvokeResult As Integer = PInvoke.scope_setAverageTerminalControl(Me._handle, Channel, Terminal_Control)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function returns selected terminal control type in the Scope mode.
    '''
    '''Note:
    '''
    '''1) This function is not available for NRP-Z51.
    '''
    '''Remote-control command(s):
    '''SENS:TRAC:AVER:TCON?
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Terminal_Control">
    '''This control returns the type of terminal control.
    '''
    '''Valid Values:
    '''Moving (RSNRPZ_TERMINAL_CONTROL_MOVING)
    '''Repeat (RSNRPZ_TERMINAL_CONTROL_REPEAT)
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function scope_getAverageTerminalControl(ByVal Channel As Integer, ByRef Terminal_Control As Integer) As Integer
        Dim pInvokeResult As Integer = PInvoke.scope_getAverageTerminalControl(Me._handle, Channel, Terminal_Control)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function determines the relative position of the trigger event in relation to the beginning of the Scope measurement sequence.
    '''
    '''Note:
    '''
    '''1) This function is not available for NRP-Z51.
    '''
    '''Remote-control command(s):
    '''SENS:TRAC:OFFS:TIME
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Offset_Time">
    '''This control sets the value of offset time.
    '''
    '''Valid Range:
    '''
    '''-5.0e-3 to 100.0 s
    '''
    '''Default Value: 0.0 s
    '''
    '''
    '''Notes:
    '''(1) Actual valid range depends on sensor used
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function scope_setOffsetTime(ByVal Channel As Integer, ByVal Offset_Time As Double) As Integer
        Dim pInvokeResult As Integer = PInvoke.scope_setOffsetTime(Me._handle, Channel, Offset_Time)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function reads the relative position of the trigger event in relation to the beginning of the Scope measurement sequence.
    '''
    '''Note:
    '''
    '''1) This function is not available for NRP-Z51.
    '''
    '''Remote-control command(s):
    '''SENS:TRAC:OFFS:TIME?
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Offset_Time">
    '''This control returns the value of offset time in seconds.
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function scope_getOffsetTime(ByVal Channel As Integer, ByRef Offset_Time As Double) As Integer
        Dim pInvokeResult As Integer = PInvoke.scope_getOffsetTime(Me._handle, Channel, Offset_Time)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function sets the number of desired values per Scope sequence.
    '''
    '''Note:
    '''
    '''1) This function is not available for NRP-Z51.
    '''
    '''Remote-control command(s):
    '''SENS:TRAC:POIN
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Scope_Points">
    '''This control sets the number of desired values per Scope sequence.
    '''
    '''Valid Range:
    '''1 to 1024
    '''
    '''Default Value: 312
    '''
    '''Notes:
    '''(1) Actual valid range depends on sensor used
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function scope_setPoints(ByVal Channel As Integer, ByVal Scope_Points As Integer) As Integer
        Dim pInvokeResult As Integer = PInvoke.scope_setPoints(Me._handle, Channel, Scope_Points)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function reads the number of desired values per Scope sequence.
    '''
    '''Note:
    '''
    '''1) This function is not available for NRP-Z51.
    '''
    '''Remote-control command(s):
    '''SENS:TRAC:POIN?
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Scope_Points">
    '''This control returns the number of desired values per Scope sequence.
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function scope_getPoints(ByVal Channel As Integer, ByRef Scope_Points As Integer) As Integer
        Dim pInvokeResult As Integer = PInvoke.scope_getPoints(Me._handle, Channel, Scope_Points)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''In the default state (OFF), each measurement sequence from the sensor is averaged over several sequences. Since the measured values of a sequence may be closer to each other in time than the measurements, several measurement sequences with a slight time offset are also superimposed on the desired sequence. With the state turned ON - this effect can be switched off, which may increase the measurement speed. This ensures that the measured values of an individual sequence are immediately delivered.
    '''
    '''Note:
    '''
    '''1) This function is not available for NRP-Z51.
    '''
    '''Remote-control command(s):
    '''SENS:TRAC:REAL ON | OFF
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Realtime">
    '''This control sets the state of real-time measurement.
    '''
    '''Valid Values:
    '''VI_TRUE  (1) - On 
    '''VI_FALSE (0) - Off (Default Value)
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function scope_setRealtimeEnabled(ByVal Channel As Integer, ByVal Realtime As Boolean) As Integer
        Dim pInvokeResult As Integer = PInvoke.scope_setRealtimeEnabled(Me._handle, Channel, System.Convert.ToUInt16(Realtime))
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function reads the state of real-time measurement setting.
    '''
    '''Note:
    '''
    '''1) This function is not available for NRP-Z51.
    '''
    '''Remote-control command(s):
    '''SENS:TRAC:REAL?
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Realtime">
    '''This control returns the state of real-time measurement.
    '''
    '''Valid Values:
    '''VI_TRUE (1) - On (Default Value)
    '''VI_FALSE (0) - Off
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function scope_getRealtimeEnabled(ByVal Channel As Integer, ByRef Realtime As Boolean) As Integer
        Dim RealtimeAsUShort As UShort
        Dim pInvokeResult As Integer = PInvoke.scope_getRealtimeEnabled(Me._handle, Channel, RealtimeAsUShort)
        Realtime = System.Convert.ToBoolean(RealtimeAsUShort)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function sets the time to be covered by the Scope sequence.
    '''
    '''Note:
    '''
    '''1) This function is not available for NRP-Z51.
    '''
    '''Remote-control command(s):
    '''SENS:TRAC:TIME
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Scope_Time">
    '''This control sets the value of scope time.
    '''
    '''Valid Range:
    '''0.1e-3 to 0.3 s
    '''
    '''Default Value: 0.01 s
    '''
    '''Notes:
    '''(1) Actual valid range depends on sensor used
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function scope_setTime(ByVal Channel As Integer, ByVal Scope_Time As Double) As Integer
        Dim pInvokeResult As Integer = PInvoke.scope_setTime(Me._handle, Channel, Scope_Time)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function reads the value of the time to be covered by the Scope sequence.
    '''
    '''Note:
    '''
    '''1) This function is not available for NRP-Z51.
    '''
    '''Remote-control command(s):
    '''SENS:TRAC:TIME?
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Scope_Time">
    '''This control returns the value of scope time in seconds.
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function scope_getTime(ByVal Channel As Integer, ByRef Scope_Time As Double) As Integer
        Dim pInvokeResult As Integer = PInvoke.scope_getTime(Me._handle, Channel, Scope_Time)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function can be used to automatically determine a value for filter bandwidth.
    '''
    '''Remote-control command(s):
    '''SENS:TRAC:AVER:COUN:AUTO ON|OFF
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Auto_Enabled">
    '''This control activates or deactivates automatic determination of a value for filter bandwidth.
    '''If the automatic switchover is activated with the ON parameter, the sensor always defines a suitable filter length.
    '''
    '''Valid Values:
    '''VI_FALSE (0) - Off
    '''VI_TRUE  (1) - On (Default Value)
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function scope_setAutoEnabled(ByVal Channel As Integer, ByVal Auto_Enabled As Boolean) As Integer
        Dim pInvokeResult As Integer = PInvoke.scope_setAutoEnabled(Me._handle, Channel, System.Convert.ToUInt16(Auto_Enabled))
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function queries the setting of automatic switchover of filter bandwidth.
    '''
    '''Remote-control command(s):
    '''SENS:TRAC:AVER:COUN:AUTO?
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Auto_Enabled">
    '''This control returns the setting of automatic switchover of filter bandwidth.
    '''
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function scope_getAutoEnabled(ByVal Channel As Integer, ByRef Auto_Enabled As Boolean) As Integer
        Dim Auto_EnabledAsUShort As UShort
        Dim pInvokeResult As Integer = PInvoke.scope_getAutoEnabled(Me._handle, Channel, Auto_EnabledAsUShort)
        Auto_Enabled = System.Convert.ToBoolean(Auto_EnabledAsUShort)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    ''' This function sets an upper time limit can be set via (maximum time). It should never be exceeded. Undesired long measurement times can thus be prevented if the automatic filter length switchover is on.
    '''
    '''Remote-control command(s):
    '''SENS:TRAC:AVER:COUN:AUTO:MTIM
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Upper_Time_Limit">
    '''This control sets the upper time limit (maximum time to fill the filter).
    '''
    '''Valid Range:
    '''
    '''NRP-21: 0.01 - 999.99
    '''FSH-Z1: 0.01 - 999.99
    '''
    '''Default Value: 4.0
    '''
    '''Notes:
    '''(1) This value is not range checked, the actual range depends on sensor used.
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function scope_setAutoMaxMeasuringTime(ByVal Channel As Integer, ByVal Upper_Time_Limit As Double) As Integer
        Dim pInvokeResult As Integer = PInvoke.scope_setAutoMaxMeasuringTime(Me._handle, Channel, Upper_Time_Limit)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function queries an upper time limit (maximum time to fill the filter).
    '''
    '''Remote-control command(s):
    '''SENS:TRAC:AVER:COUN:AUTO:MTIM
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Upper_Time_Limit">
    '''This control returns an upper time limit (maximum time to fill the filter).
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function scope_getAutoMaxMeasuringTime(ByVal Channel As Integer, ByRef Upper_Time_Limit As Double) As Integer
        Dim pInvokeResult As Integer = PInvoke.scope_getAutoMaxMeasuringTime(Me._handle, Channel, Upper_Time_Limit)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function sets the maximum noise ratio in the measurement result.
    '''
    '''Remote-control command(s):
    '''SENS:TRAC:AVER:COUN:AUTO:NSR
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Maximum_Noise_Ratio">
    '''This control sets the maximum noise ratio in the measurement result. The value is set in dB.
    '''
    '''Valid Range:
    '''
    '''NRP-Z21: 0.0 - 1.0
    '''FSH-Z1:  0.0 - 1.0
    '''
    '''Default Value: 0.1
    '''
    '''Notes:
    '''(1) This value is not range checked; the actual range depends on sensor used.
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function scope_setAutoNoiseSignalRatio(ByVal Channel As Integer, ByVal Maximum_Noise_Ratio As Double) As Integer
        Dim pInvokeResult As Integer = PInvoke.scope_setAutoNoiseSignalRatio(Me._handle, Channel, Maximum_Noise_Ratio)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function queries the maximum noise signal ratio value.
    '''
    '''Remote-control command(s):
    '''SENS:TRAC:AVER:COUN:AUTO:NSR?
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Maximum_Noise_Ratio">
    '''This control returns a maximum noise signal ratio in dB.
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function scope_getAutoNoiseSignalRatio(ByVal Channel As Integer, ByRef Maximum_Noise_Ratio As Double) As Integer
        Dim pInvokeResult As Integer = PInvoke.scope_getAutoNoiseSignalRatio(Me._handle, Channel, Maximum_Noise_Ratio)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function defines the number of significant places for linear units and the number of decimal places for logarithmic units which should be free of noise in the measurement result. This setting does not affect the setting of display.
    '''
    '''Remote-control command(s):
    '''SENS:AVER:COUN:AUTO:RES 1 ... 4
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Resolution">
    '''This control defines the number of significant places for linear units and the number of decimal places for logarithmic units which should be free of noise in the measurement result.
    '''
    '''Valid Range:
    '''1 to 4
    '''
    '''Default Value: 3
    '''
    '''Notes:
    '''(1) Actual range depend on sensor used and may vary from the range stated above.
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function scope_setAutoResolution(ByVal Channel As Integer, ByVal Resolution As Integer) As Integer
        Dim pInvokeResult As Integer = PInvoke.scope_setAutoResolution(Me._handle, Channel, Resolution)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function returns the number of significant places for linear units and the number of decimal places for logarithmic units which should be free of noise in the measurement result. 
    '''
    '''Remote-control command(s):
    '''SENS:AVER:COUN:AUTO:RES?
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Resolution">
    '''This control returns the number of significant places for linear units and the number of decimal places for logarithmic units which should be free of noise in the measurement result.
    '''
    '''Valid Range:
    '''1 to 4
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''
    '''</returns>
    Public Function scope_getAutoResolution(ByVal Channel As Integer, ByRef Resolution As Integer) As Integer
        Dim pInvokeResult As Integer = PInvoke.scope_getAutoResolution(Me._handle, Channel, Resolution)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function selects a method by which the automatic filter length switchover can operate.
    '''
    '''Remote-control command(s):
    '''SENS:TRAC:AVER:COUN:AUTO:TYPE RES | NSR
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Method">
    '''This control selects a method by which the automatic filter length switchover can operate.
    '''
    '''Valid Values:
    '''RSNRPZ_AUTO_COUNT_TYPE_RESOLUTION (0) - Resolution (Default Value)
    '''RSNRPZ_AUTO_COUNT_TYPE_NSR (1) - Noise Ratio
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function scope_setAutoType(ByVal Channel As Integer, ByVal Method As Integer) As Integer
        Dim pInvokeResult As Integer = PInvoke.scope_setAutoType(Me._handle, Channel, Method)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function returns a method by which the automatic filter length switchover can operate.
    '''
    '''Remote-control command(s):
    '''SENS:TRAC:AVER:COUN:AUTO:TYPE?
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Method">
    '''This control returns a method by which the automatic filter length switchover can operate.
    '''
    '''Valid Values:
    '''Resolution (RSNRPZ_AUTO_COUNT_TYPE_RESOLUTION)
    '''Noise Ratio (RSNRPZ_AUTO_COUNT_TYPE_NSR)
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function scope_getAutoType(ByVal Channel As Integer, ByRef Method As Integer) As Integer
        Dim pInvokeResult As Integer = PInvoke.scope_getAutoType(Me._handle, Channel, Method)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function activates or deactivates the automatic equivalent sampling if the resolution of the trace measurement is configured lower than a sample period.
    '''
    '''Note:
    '''1) This function is only available for NRP-Z81.
    '''
    '''Remote-control command(s):
    '''SENS:TRAC:ESAM:AUTO
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Scope_Equivalent_Sampling">
    '''This control activates or deactivates the automatic equivalent sampling if the resolution of the trace measurement is configured lower than a sample period.
    '''
    '''Valid Values:
    '''VI_TRUE  (1) - On (Default Value)
    '''VI_FALSE (0) - Off
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function scope_setEquivalentSampling(ByVal Channel As Integer, ByVal Scope_Equivalent_Sampling As Boolean) As Integer
        Dim pInvokeResult As Integer = PInvoke.scope_setEquivalentSampling(Me._handle, Channel, System.Convert.ToUInt16(Scope_Equivalent_Sampling))
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function returns the state of the automatic equivalent sampling if the resolution of the trace measurement is configured lower than a sample period.
    '''
    '''Note:
    '''1) This function is only available for NRP-Z81.
    '''
    '''Remote-control command(s):
    '''SENS:TRAC:ESAM:AUTO?
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Scope_Equivalent_Sampling">
    '''This control returns the state of the automatic equivalent sampling if the resolution of the trace measurement is configured lower than a sample period.
    '''
    '''Valid Values:
    '''VI_TRUE  (1) - On
    '''VI_FALSE (0) - Off
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function scope_getEquivalentSampling(ByVal Channel As Integer, ByRef Scope_Equivalent_Sampling As Boolean) As Integer
        Dim Scope_Equivalent_SamplingAsUShort As UShort
        Dim pInvokeResult As Integer = PInvoke.scope_getEquivalentSampling(Me._handle, Channel, Scope_Equivalent_SamplingAsUShort)
        Scope_Equivalent_Sampling = System.Convert.ToBoolean(Scope_Equivalent_SamplingAsUShort)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function turns on or off the automatic pulse measurement feature. When traceMeasurements is set to on, the sensor tries to compute the pulse parameters for the current measured trace.
    '''
    '''Note:
    '''1) This function is only available for NRP-Z81.
    '''
    '''Remote-control command(s):
    '''SENS:TRAC:MEAS:STAT ON | OFF
    '''SENS:TRAC:MEAS:AUTO ON | OFF
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Trace_Measurements">
    '''This control switches the automatic pulse measurement feature.
    '''
    '''Valid Values:
    '''VI_TRUE  (1) - On (Default Value)
    '''VI_FALSE (0) - Off
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function scope_meas_setMeasEnabled(ByVal Channel As Integer, ByVal Trace_Measurements As Boolean) As Integer
        Dim pInvokeResult As Integer = PInvoke.scope_meas_setMeasEnabled(Me._handle, Channel, System.Convert.ToUInt16(Trace_Measurements))
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function queries the state of the automatic pulse measurement feature.
    '''
    '''Note:
    '''1) This function is only available for NRP-Z81.
    '''
    '''Remote-control command(s):
    '''SENS:TRAC:MEAS:STAT?
    '''SENS:TRAC:MEAS:TRAN:AUTO?
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Trace_Measurements">
    '''This control returns the state of the automatic pulse measurement feature.
    '''
    '''Valid Values:
    '''VI_TRUE  (1) - On
    '''VI_FALSE (0) - Off
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function scope_meas_getMeasEnabled(ByVal Channel As Integer, ByRef Trace_Measurements As Boolean) As Integer
        Dim Trace_MeasurementsAsUShort As UShort
        Dim pInvokeResult As Integer = PInvoke.scope_meas_getMeasEnabled(Me._handle, Channel, Trace_MeasurementsAsUShort)
        Trace_Measurements = System.Convert.ToBoolean(Trace_MeasurementsAsUShort)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function selects the algorithm for detecting the top and the base level of the pulsed signal.
    '''
    '''Note:
    '''1) This function is only available for NRP-Z81.
    '''
    '''Remote-control command(s):
    '''SENS:TRAC:MEAS:ALG HIST | INT
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Algorithm">
    '''This control selects the algorithm for detecting the top and the base level of the pulsed signal.
    '''
    '''Valid Values:
    '''RSNRPZ_SCOPE_MEAS_ALG_HIST (0) - Histogram (Default Value)
    '''RSNRPZ_SCOPE_MEAS_ALG_INT  (1) - Integral
    '''
    '''Note(s):
    '''
    '''(1) Histogram - The Histogram Algorithm computes the pulse levels analysing the Histogram of the trace data. Toplevel and Baselevel are the bins with the maximum number of hits in the upper and the lower half of the histogram.
    '''If  the signal has too much noise that there is no maximum bin, the algorithm returns the min and max peak sample values as base and top level.
    '''
    '''(2) Integration - The Integration Algorithm tries to find the pulse top power by fitting a reference rectangle pulse into the pulse by doing the integration of the pulse power and the according voltages. This algorithm should be used if the energy content of the complete pulse (including rising and falling edges) is needed and not only the most probable top level.
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function scope_meas_setMeasAlgorithm(ByVal Channel As Integer, ByVal Algorithm As Integer) As Integer
        Dim pInvokeResult As Integer = PInvoke.scope_meas_setMeasAlgorithm(Me._handle, Channel, Algorithm)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function queries selected algorithm for detecting the top and the base level of the pulsed signal.
    '''
    '''Note:
    '''1) This function is only available for NRP-Z81.
    '''
    '''Remote-control command(s):
    '''SENS:TRAC:MEAS:ALG?
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Algorithm">
    '''This control returns selected algorithm for detecting the top and the base level of the pulsed signal.
    '''
    '''Valid Values:
    '''RSNRPZ_SCOPE_MEAS_ALG_HIST (0) - Histogram
    '''RSNRPZ_SCOPE_MEAS_ALG_INT  (1) - Integral
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function scope_meas_getMeasAlgorithm(ByVal Channel As Integer, ByRef Algorithm As Integer) As Integer
        Dim pInvokeResult As Integer = PInvoke.scope_meas_getMeasAlgorithm(Me._handle, Channel, Algorithm)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function defines the thresholds of the reference and transition levels that are used for the calculation  of the pulse's time parameter.
    '''The duration reference level is used to calculate pulse duration and pulse period, the transition low and high levels are used to calculate the pulse transition?s rise/fall time and their occurrences.
    '''
    '''Note:
    '''1) This function is only available for NRP-Z81.
    '''
    '''Remote-control command(s):
    '''SENS:TRAC:MEAS:DEF:DUR:REF 
    '''SENS:TRAC:MEAS:DEF:TRAN:LREF
    '''SENS:TRAC:MEAS:DEF:TRAN:HREF
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Duration_Ref">
    '''This control defines duration reference level used to calculate pulse duration and pulse period.
    '''
    '''Valid Range:
    '''0.0 - 100.0 %
    '''
    '''Default Value: 50.0 %
    '''
    '''</param>
    '''<param name="Transition_Low_Ref">
    '''This control defines transition low level used to calculate the pulse transition's rise time and its occurrences.
    '''
    '''Valid Range:
    '''0.0 - 100.0 %
    '''
    '''Default Value: 10.0 %
    '''</param>
    '''<param name="Transition_High_Ref">
    '''This control defines transition high level used to calculate the pulse transition's fall time and its occurrences.
    '''
    '''Valid Range:
    '''0.0 - 100.0 %
    '''
    '''Default Value: 90.0 %
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function scope_meas_setLevelThresholds(ByVal Channel As Integer, ByVal Duration_Ref As Double, ByVal Transition_Low_Ref As Double, ByVal Transition_High_Ref As Double) As Integer
        Dim pInvokeResult As Integer = PInvoke.scope_meas_setLevelThresholds(Me._handle, Channel, Duration_Ref, Transition_Low_Ref, Transition_High_Ref)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function queries the thresholds of the reference and transition levels that are used for the calculation  of the pulse's time parameter.
    '''
    '''Note:
    '''1) This function is only available for NRP-Z81.
    '''
    '''Remote-control command(s):
    '''SENS:TRAC:MEAS:DEF:DUR:REF? 
    '''SENS:TRAC:MEAS:DEF:TRAN:LREF?
    '''SENS:TRAC:MEAS:DEF:TRAN:HREF?
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Duration_Ref">
    '''This control defines duration reference level.
    '''
    '''Valid Range:
    '''0.0 - 100.0 %
    '''</param>
    '''<param name="Transition_Low_Ref">
    '''This control returns transition low level.
    '''
    '''Valid Range:
    '''0.0 - 100.0 %
    '''</param>
    '''<param name="Transition_High_Ref">
    '''This control returns transition high level.
    '''
    '''Valid Range:
    '''0.0 - 100.0 %
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function scope_meas_getLevelThresholds(ByVal Channel As Integer, ByRef Duration_Ref As Double, ByRef Transition_Low_Ref As Double, ByRef Transition_High_Ref As Double) As Integer
        Dim pInvokeResult As Integer = PInvoke.scope_meas_getLevelThresholds(Me._handle, Channel, Duration_Ref, Transition_Low_Ref, Transition_High_Ref)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function defines measurement time which sets the duration of analysing the current trace for the pulse parameters. The measurement time could be used together with the measurement offset time to select the second (or any other) pulse in the trace and not the whole trace.
    '''
    '''Note:
    '''1) This function is only available for NRP-Z81.
    '''
    '''Remote-control command(s):
    '''SENSe:TRACe:MEAS:TIME
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Meas_Time">
    '''The measurement time is used to set the duration of analysing the current trace for the pulse parameters. The measurement time could be used together with the measurement offset time to select the second (or any other) pulse in the trace and not the whole trace.
    '''
    '''To disable this "time gate" set the measurement time to 0.0.
    '''
    '''Valid Range:
    '''-
    '''
    '''Default Value: 0.0 s
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function scope_meas_setTime(ByVal Channel As Integer, ByVal Meas_Time As Double) As Integer
        Dim pInvokeResult As Integer = PInvoke.scope_meas_setTime(Me._handle, Channel, Meas_Time)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function returns the measurement time.
    '''
    '''Note:
    '''1) This function is only available for NRP-Z81.
    '''
    '''Remote-control command(s):
    '''SENSe:TRACe:MEAS:TIME?
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Meas_Time">
    '''This control returns the measurement time.
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function scope_meas_getTime(ByVal Channel As Integer, ByRef Meas_Time As Double) As Integer
        Dim pInvokeResult As Integer = PInvoke.scope_meas_getTime(Me._handle, Channel, Meas_Time)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function defines offset time used to set the start point of analysing the current trace for the pulse parameters. The offset time could be used to start analysis from the second (or any other) pulse occurrence in the trace and not from the beginning of the trace.
    '''
    '''Note:
    '''1) This function is only available for NRP-Z81.
    '''
    '''Remote-control command(s):
    '''SENSe:TRACe:MEAS:OFFS:TIME
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Offset_Time">
    '''This control defines offset time used to set the start point of analysing the current trace for the pulse parameters. The offset time could be used to start analysis from the second (or any other) pulse occurrence in the trace and not from the beginning of the trace.
    '''
    '''Valid Range:
    '''0 - 0.99 s
    '''
    '''Default Value: 0.0 s
    '''
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function scope_meas_setOffsetTime(ByVal Channel As Integer, ByVal Offset_Time As Double) As Integer
        Dim pInvokeResult As Integer = PInvoke.scope_meas_setOffsetTime(Me._handle, Channel, Offset_Time)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function queries offset time used to set the start point of analysing the current trace for the pulse parameters.
    '''
    '''Note:
    '''1) This function is only available for NRP-Z81.
    '''
    '''Remote-control command(s):
    '''SENSe:TRACe:MEAS:OFFS:TIME?
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Offset_Time">
    '''This control returns offset time used to set the start point of analysing the current trace for the pulse parameters.
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function scope_meas_getOffsetTime(ByVal Channel As Integer, ByRef Offset_Time As Double) As Integer
        Dim pInvokeResult As Integer = PInvoke.scope_meas_getOffsetTime(Me._handle, Channel, Offset_Time)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function returns the calculated pulse time parameters of the last recorded trace. If a parameter could not be calculated the returned value is NAN. The Sensor takes the time values when the trace crosses the reference level points for duration and period calculation.
    '''
    '''Note:
    '''1) This function is only available for NRP-Z81.
    '''
    '''Remote-control command(s):
    '''SENS:TRAC:MEAS:PULS:DCYC?
    '''SENS:TRAC:MEAS:PULS:DUR?
    '''SENS:TRAC:MEAS:PULS:PER?
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Duty_Cycle">
    '''This control returns duty cycle value. Duty Cycle = (pulse duration / pulse period) * 100
    '''</param>
    '''<param name="Pulse_Duration">
    '''This control returns pulse duration value. Pulse Duration is the time between the positive and the negative transition of one pulse.
    '''</param>
    '''<param name="Pulse_Period">
    '''This control returns pulse period value. Pulse Period is the time between two consecutive transitions of the same polarity.
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function scope_meas_getPulseTimes(ByVal Channel As Integer, ByRef Duty_Cycle As Double, ByRef Pulse_Duration As Double, ByRef Pulse_Period As Double) As Integer
        Dim pInvokeResult As Integer = PInvoke.scope_meas_getPulseTimes(Me._handle, Channel, Duty_Cycle, Pulse_Duration, Pulse_Period)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function returns the transition parameters of the last examined trace data.
    '''The NRP Sensor always searches for the first rising edge and the first falling edge in the trace. If offset time is set greater than  zero the algorithm searches the edges from this time in the trace.
    '''
    '''Note:
    '''1) This function is only available for NRP-Z81.
    '''
    '''Remote-control command(s):
    '''SENS:TRAC:MEAS:TRAN:POS:DUR?
    '''SENS:TRAC:MEAS:TRAN:POS:OCC?
    '''SENS:TRAC:MEAS:TRAN:POS:OVER?
    '''SENS:TRAC:MEAS:TRAN:NEG:DUR?
    '''SENS:TRAC:MEAS:TRAN:NEG:OCC?
    '''SENS:TRAC:MEAS:TRAN:NEG:OVER?
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Slope">
    '''Selects measured transition.
    '''
    '''Valid Values:
    '''RSNRPZ_SLOPE_POSITIVE (0) - Positive (Default Value)
    '''RSNRPZ_SLOPE_NEGATIVE (1) - Negative
    '''</param>
    '''<param name="Duration">
    '''This control returns transition duration value. The positive transition duration is measured from the point when the trace crosses the low reference level until it reaches the high reference level. Negative transition is vice versa.
    '''</param>
    '''<param name="Occurence">
    '''This control returns transition occurence value. The positive transition occurrence is the absolut time of the trace when it crosses the medial reference level.
    '''</param>
    '''<param name="Overshoot">
    '''This control returns overshoot value. The overshoot measures the height of the local maximum (minimum) following a rising (falling) transition. Overshoot is calculated in percent of the pulse amplitude (top level - base level).
    '''
    '''Overshoot (pos) = 100 * (local maximum - top level) / (top level - base level)
    '''Overshoot (neg) = 100 * (base level - local minimum) / (top level - base level)
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function scope_meas_getPulseTransition(ByVal Channel As Integer, ByVal Slope As Integer, ByRef Duration As Double, ByRef Occurence As Double, ByRef Overshoot As Double) As Integer
        Dim pInvokeResult As Integer = PInvoke.scope_meas_getPulseTransition(Me._handle, Channel, Slope, Duration, Occurence, Overshoot)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function returns the average power, the minimum level and the maximum level of the analysed trace in Watt.
    '''
    '''Note(s):
    '''1) This function is only available for NRP-Z81.
    '''
    '''Remote-control command(s):
    '''SENS:TRAC:MEAS:POW:AVG?
    '''SENS:TRAC:MEAS:POW:MIN?
    '''SENS:TRAC:MEAS:POW:MAX?
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Average">
    '''This control returns average power value.
    '''</param>
    '''<param name="Min_Peak">
    '''This control returns min peak power value.
    '''</param>
    '''<param name="Max_Peak">
    '''This control returns max peak power value.
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function scope_meas_getPulsePower(ByVal Channel As Integer, ByRef Average As Double, ByRef Min_Peak As Double, ByRef Max_Peak As Double) As Integer
        Dim pInvokeResult As Integer = PInvoke.scope_meas_getPulsePower(Me._handle, Channel, Average, Min_Peak, Max_Peak)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function returns the pulse top level and the pulse base level in watt. Both levels are calculated with the algorithm that was set with the rsnrpz_scope_meas_setMeasAlgorithm(...)  function.
    '''
    '''Note:
    '''1) This function is only available for NRP-Z81.
    '''
    '''Remote-control command(s):
    '''SENS:TRAC:MEAS:POW:PULS:TOP?
    '''SENS:TRAC:MEAS:POW:PULS:BASE?
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Top_Level">
    '''This control returns top level value.
    '''</param>
    '''<param name="Base_Level">
    '''This control returns base level value.
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function scope_meas_getPulseLevels(ByVal Channel As Integer, ByRef Top_Level As Double, ByRef Base_Level As Double) As Integer
        Dim pInvokeResult As Integer = PInvoke.scope_meas_getPulseLevels(Me._handle, Channel, Top_Level, Base_Level)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function returns the calculated reference level in Watt at the defined thresholds of the last recorded trace. The thresholds in percent are defined with the function  rsnrpz_scope_meas_setLevelThresholds(?) of the pulse amplitude.
    '''
    '''Note:
    '''1) This function is only available for NRP-Z81.
    '''
    '''Remote-control command(s):
    '''SENS:TRAC:MEAS:POW:LREF?
    '''SENS:TRAC:MEAS:POW:HREF?
    '''SENS:TRAC:MEAS:POW:REF?
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Low_Ref_Level">
    '''This control returns absolute power at the 10% amplitude level.
    '''</param>
    '''<param name="High_Ref_Level">
    '''This control returns absolute power at the 90% amplitude level.
    '''</param>
    '''<param name="Duration_Ref_Level">
    '''This control returns the absolute power at the 50% amplitude level.
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function scope_meas_getPulseReferenceLevels(ByVal Channel As Integer, ByRef Low_Ref_Level As Double, ByRef High_Ref_Level As Double, ByRef Duration_Ref_Level As Double) As Integer
        Dim pInvokeResult As Integer = PInvoke.scope_meas_getPulseReferenceLevels(Me._handle, Channel, Low_Ref_Level, High_Ref_Level, Duration_Ref_Level)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function activates or deactivates the automatic equivalent sampling mode during automatic pulse analysis.
    '''If equivalent sampling is enabled, the sensor tries to measure the time parameters (mainly rise/fall times) of the pulse with a high resolution by doing equivalent sampling.
    '''To do the equivalent sampling a periodic signal is mandatory. The sensor decides automatically if equivalent  sampling is possible. To get the resolution of the measured time parameter the function "rsnrpz_scope_meas_getSamplePeriod" could be used.
    '''
    '''Note:
    '''1) This function is only available for NRP-Z81.
    '''
    '''Remote-control command(s):
    '''SENS:TRAC:MEAS:TRANS:ESAM:AUTO
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Scope_Meas_Equiv_Sampling">
    '''This control activates or deactivates the automatic equivalent sampling mode during automatic pulse analysis.
    '''
    '''Valid Values:
    '''VI_TRUE  (1) - On (Default Value)
    '''VI_FALSE (0) - Off
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function scope_meas_setEquivalentSampling(ByVal Channel As Integer, ByVal Scope_Meas_Equiv_Sampling As Boolean) As Integer
        Dim pInvokeResult As Integer = PInvoke.scope_meas_setEquivalentSampling(Me._handle, Channel, System.Convert.ToUInt16(Scope_Meas_Equiv_Sampling))
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function returns the state of the automatic equivalent sampling mode during automatic pulse analysis.
    '''
    '''Note:
    '''1) This function is only available for NRP-Z81.
    '''
    '''Remote-control command(s):
    '''SENS:TRAC:MEAS:TRANS:ESAM:AUTO?
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Scope_Meas_Equiv_Sampling">
    '''This control returns the state of the automatic equivalent sampling mode during automatic pulse analysis.
    '''
    '''Valid Values:
    '''VI_TRUE  (1) - On
    '''VI_FALSE (0) - Off
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function scope_meas_getEquivalentSampling(ByVal Channel As Integer, ByRef Scope_Meas_Equiv_Sampling As Boolean) As Integer
        Dim Scope_Meas_Equiv_SamplingAsUShort As UShort
        Dim pInvokeResult As Integer = PInvoke.scope_meas_getEquivalentSampling(Me._handle, Channel, Scope_Meas_Equiv_SamplingAsUShort)
        Scope_Meas_Equiv_Sampling = System.Convert.ToBoolean(Scope_Meas_Equiv_SamplingAsUShort)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function returns the sample period (in s) of the last pulse analysis. This parameter is a good indicator if the equivalent sampling mode of measuring the rise and fall times was successful or not.
    '''
    '''Note:
    '''1) This function is only available for NRP-Z81.
    '''
    '''Remote-control command(s):
    '''SENS:TRAC:MEAS:TRANS:SPER?
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Sample_Period">
    '''This function returns the sample period (in s) of the last pulse analysis.
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function scope_meas_getSamplePeriod(ByVal Channel As Integer, ByRef Sample_Period As Double) As Integer
        Dim pInvokeResult As Integer = PInvoke.scope_meas_getSamplePeriod(Me._handle, Channel, Sample_Period)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function configures the parameters of internal trigger system.
    '''
    '''Remote-control command(s):
    '''TRIG:DEL:AUTO ON
    '''TRIG:ATR OFF
    '''TRIG:COUN 1
    '''TRIG:DEL 0.0
    '''TRIG:HOLD 0.0
    '''TRIG:HYST 3DB
    '''TRIG:LEV &lt;value&gt;
    '''TRIG:SLOP POS | NEG
    '''TRIG:SOUR INT
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Trigger_Level">
    '''This control determines the power (in W) a trigger signal must exceed before a trigger event is detected.
    '''
    '''Valid Range:
    '''0.1e-6 to 0.2 W
    '''
    '''Default Value:
    '''1.0e-6 W
    '''
    '''Notes:
    '''(1) Actual valid range depends on sensor used
    '''</param>
    '''<param name="Trigger_Slope">
    '''This control determines whether the rising (POSitive) or the falling (NEGative) edge of the signal is used for triggering.
    '''
    '''Valid Values:
    '''RSNRPZ_SLOPE_POSITIVE (0) - Positive (Default Value)
    '''RSNRPZ_SLOPE_NEGATIVE (1) - Negative
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function trigger_configureInternal(ByVal Channel As Integer, ByVal Trigger_Level As Double, ByVal Trigger_Slope As Integer) As Integer
        Dim pInvokeResult As Integer = PInvoke.trigger_configureInternal(Me._handle, Channel, Trigger_Level, Trigger_Slope)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function configures the parameters of external trigger system.
    '''
    '''Remote-control command(s):
    '''TRIG:DEL &lt;value&gt;
    '''TRIG:SOUR EXT
    '''TRIG:COUN 1
    '''TRIG:ATR OFF
    '''TRIG:HOLD 0.0
    '''TRIG:DEL:AUTO ON
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Trigger_Delay">
    '''This control sets a the delay (in seconds) between the trigger event and the beginning of the actual measurement (integration).
    '''
    '''Valid Range:
    '''-5.0e-3 to 100.0 s
    '''
    '''Default Value:
    '''0.0 s
    '''
    '''Notes:
    '''(1) Actual valid range depends on sensor used
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function trigger_configureExternal(ByVal Channel As Integer, ByVal Trigger_Delay As Double) As Integer
        Dim pInvokeResult As Integer = PInvoke.trigger_configureExternal(Me._handle, Channel, Trigger_Delay)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function performs triggering and ensures that the sensor directly changes from the WAIT_FOR_TRG state to the MEASURING state irrespective of the selected trigger source. A trigger delay set with TRIG:DEL is ignored but not the automatic delay determined when TRIG:DEL:AUTO:ON is set.
    '''When the trigger source is HOLD, a measurement can only be started with TRIG.
    '''
    '''Remote-control command(s):
    '''TRIG:IMM
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function trigger_immediate(ByVal Channel As Integer) As Integer
        Dim pInvokeResult As Integer = PInvoke.trigger_immediate(Me._handle, Channel)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function ensures (if parameter is set to On) by means of an automatically determined delay that a measurement is started only after the sensor has settled. This is important when thermal sensors are used.
    '''
    '''Remote-control command(s):
    '''TRIG:DEL:AUTO ON | OFF
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Auto_Delay">
    '''This control enables or disables automatic determination of delay.
    '''
    '''Valid Values:
    '''VI_TRUE  (1) - On
    '''VI_FALSE (0) - Off (Default Value)
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function trigger_setAutoDelayEnabled(ByVal Channel As Integer, ByVal Auto_Delay As Boolean) As Integer
        Dim pInvokeResult As Integer = PInvoke.trigger_setAutoDelayEnabled(Me._handle, Channel, System.Convert.ToUInt16(Auto_Delay))
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function reads the setting of auto delay feature.
    '''
    '''Remote-control command(s):
    '''TRIG:DEL:AUTO?
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Auto_Delay">
    '''This control returns the state of Auto Delay feature.
    '''
    '''Valid Values:
    '''VI_TRUE  (1) - On
    '''VI_FALSE (0) - Off
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function trigger_getAutoDelayEnabled(ByVal Channel As Integer, ByRef Auto_Delay As Boolean) As Integer
        Dim Auto_DelayAsUShort As UShort
        Dim pInvokeResult As Integer = PInvoke.trigger_getAutoDelayEnabled(Me._handle, Channel, Auto_DelayAsUShort)
        Auto_Delay = System.Convert.ToBoolean(Auto_DelayAsUShort)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function turns On or Off the auto trigger feature. When auto trigger is set to On, the WAIT_FOR_TRG state is automatically exited when no trigger event occurs within a period that corresponds to the reciprocal of the display update rate.
    '''
    '''Note:
    '''  
    '''1) This function is not available for NRP-Z51.
    '''
    '''Remote-control command(s):
    '''TRIG:ATR:STAT ON | OFF
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Auto_Trigger">
    '''This control enables or disables the Auto Trigger.
    '''
    '''Valid Values:
    '''VI_TRUE  (1) - On
    '''VI_FALSE (0) - Off (Default Value)
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function trigger_setAutoTriggerEnabled(ByVal Channel As Integer, ByVal Auto_Trigger As Boolean) As Integer
        Dim pInvokeResult As Integer = PInvoke.trigger_setAutoTriggerEnabled(Me._handle, Channel, System.Convert.ToUInt16(Auto_Trigger))
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function reads the state of Auto Trigger.
    '''
    '''Note:
    '''  
    '''1) This function is not available for NRP-Z51.
    '''
    '''Remote-control command(s):
    '''TRIG:ATR:STAT?
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Auto_Trigger">
    '''This control returns the state of Auto Trigger.
    '''
    '''Valid Values:
    '''VI_TRUE (1) - On
    '''VI_FALSE (0) - Off
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function trigger_getAutoTriggerEnabled(ByVal Channel As Integer, ByRef Auto_Trigger As Boolean) As Integer
        Dim Auto_TriggerAsUShort As UShort
        Dim pInvokeResult As Integer = PInvoke.trigger_getAutoTriggerEnabled(Me._handle, Channel, Auto_TriggerAsUShort)
        Auto_Trigger = System.Convert.ToBoolean(Auto_TriggerAsUShort)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function sets the number of measurement cycles to be  performed when the measurement is started with INIT.
    '''
    '''Remote-control command(s):
    '''TRIG:COUN
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Trigger_Count">
    '''This control sets the number of measurement cycles to be  performed when the measurement is started with INIT.
    '''
    '''Valid Range:
    '''1 to 2147483646
    '''
    '''Default Value:
    '''1
    '''
    '''Notes:
    '''(1) Actual valid range depends on sensor used
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function trigger_setCount(ByVal Channel As Integer, ByVal Trigger_Count As Integer) As Integer
        Dim pInvokeResult As Integer = PInvoke.trigger_setCount(Me._handle, Channel, Trigger_Count)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function reads the number of measurement cycles to be  performed when the measurement is started with INIT.
    '''
    '''Remote-control command(s):
    '''TRIG:COUN?
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Trigger_Count">
    '''This control returns the number of measurement cycles to be  performed when the measurement is started with INIT.
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function trigger_getCount(ByVal Channel As Integer, ByRef Trigger_Count As Integer) As Integer
        Dim pInvokeResult As Integer = PInvoke.trigger_getCount(Me._handle, Channel, Trigger_Count)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function defines the delay between the trigger event and the beginning of the actual measurement (integration).
    '''
    '''Remote-control command(s):
    '''TRIG:DEL
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Trigger_Delay">
    '''This control sets a the delay (in seconds) between the trigger event and the beginning of the actual measurement (integration).
    '''
    '''Valid Range:
    '''NRP-Z21: -5.0e-3 to 100.0 s
    '''NRP-Z51:  0.0    to 100.0 s
    '''FSH-Z1:  -5.0e-3 to 100.0 s
    '''
    '''Default Value:
    '''0.0 s
    '''
    '''Notes:
    '''(1) Actual valid range depends on sensor used
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function trigger_setDelay(ByVal Channel As Integer, ByVal Trigger_Delay As Double) As Integer
        Dim pInvokeResult As Integer = PInvoke.trigger_setDelay(Me._handle, Channel, Trigger_Delay)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function reads value of the delay (in seconds) between the trigger event and the beginning of the actual measurement (integration).
    '''
    '''Remote-control command(s):
    '''TRIG:DEL?
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Trigger_Delay">
    '''This control returns value of the delay (in seconds) between the trigger event and the beginning of the actual measurement (integration).
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function trigger_getDelay(ByVal Channel As Integer, ByRef Trigger_Delay As Double) As Integer
        Dim pInvokeResult As Integer = PInvoke.trigger_getDelay(Me._handle, Channel, Trigger_Delay)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function defines a period after a trigger event within which all further trigger events are ignored.
    '''
    '''Remote-control command(s):
    '''TRIG:HOLD
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Trigger_Holdoff">
    '''This control defines a period (in seconds) after a trigger event within which all further trigger events are ignored.
    '''
    '''Valid Range:
    '''0.0 - 10.0 s
    '''
    '''Default Value:
    '''0.0 s
    '''
    '''Notes:
    '''(1) Actual valid range depends on sensor used
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function trigger_setHoldoff(ByVal Channel As Integer, ByVal Trigger_Holdoff As Double) As Integer
        Dim pInvokeResult As Integer = PInvoke.trigger_setHoldoff(Me._handle, Channel, Trigger_Holdoff)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function returns the value of a period after a trigger event within which all further trigger events are ignored.
    '''
    '''Remote-control command(s):
    '''TRIGger[1..4]:HOLDoff?
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Trigger_Holdoff">
    '''This control returns the value of a period (in seconds) after a trigger event within which all further trigger events are ignored.
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function trigger_getHoldoff(ByVal Channel As Integer, ByRef Trigger_Holdoff As Double) As Integer
        Dim pInvokeResult As Integer = PInvoke.trigger_getHoldoff(Me._handle, Channel, Trigger_Holdoff)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function is used to specify how far the signal level has to drop below the trigger level before a new signal edge can be detected as a trigger event. Thus, this command can be used to eliminate the effects of noise in the signal on the transition filters of the trigger system.
    '''
    '''Remote-control command(s):
    '''TRIG:HYST
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Trigger_Hysteresis">
    '''This control defines the trigger hysteresis in dB.
    '''
    '''Valid Range:
    '''0.0 to 10.0 dB
    '''
    '''Default Value: 0.0 dB
    '''
    '''Notes:
    '''(1) Actual valid range depends on sensor used
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function trigger_setHysteresis(ByVal Channel As Integer, ByVal Trigger_Hysteresis As Double) As Integer
        Dim pInvokeResult As Integer = PInvoke.trigger_setHysteresis(Me._handle, Channel, Trigger_Hysteresis)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function reads the value of trigger hysteresis.
    '''
    '''Remote-control command(s):
    '''TRIG:HYST?
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Trigger_Hysteresis">
    '''This control returns the value of trigger hysteresis in dB.
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function trigger_getHysteresis(ByVal Channel As Integer, ByRef Trigger_Hysteresis As Double) As Integer
        Dim pInvokeResult As Integer = PInvoke.trigger_getHysteresis(Me._handle, Channel, Trigger_Hysteresis)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function determines the power a trigger signal must exceed before a trigger event is detected. This setting is only used for internal trigger signal source.
    '''
    '''Remote-control command(s):
    '''TRIG:LEV
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Trigger_Level">
    '''This control determines the power (in W) a trigger signal must exceed before a trigger event is detected.
    '''
    '''Valid Range:
    '''NRP-Z21: 0.1e-6  to 0.2 W
    '''NRP-Z51: 0.25e-6 to 0.1 W
    '''FSH-Z1:  0.1e-6  to 0.2 W
    '''
    '''Default Value:
    '''1.0e-6 W
    '''
    '''Notes:
    '''(1) Actual valid range depends on sensor used
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function trigger_setLevel(ByVal Channel As Integer, ByVal Trigger_Level As Double) As Integer
        Dim pInvokeResult As Integer = PInvoke.trigger_setLevel(Me._handle, Channel, Trigger_Level)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function reads the power a trigger signal must exceed before a trigger event is detected.
    '''
    '''Remote-control command(s):
    '''TRIG:LEV?
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Trigger_Level">
    '''This control returns the power (in W) a trigger signal must exceed before a trigger event is detected.
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function trigger_getLevel(ByVal Channel As Integer, ByRef Trigger_Level As Double) As Integer
        Dim pInvokeResult As Integer = PInvoke.trigger_getLevel(Me._handle, Channel, Trigger_Level)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function determines whether the rising (POSitive) or the falling (NEGative) edge of the signal is used for triggering.
    '''
    '''Remote-control command(s):
    '''TRIG:SLOP POSitive | NEGative
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Trigger_Slope">
    '''This control determines whether the rising (POSitive) or the falling (NEGative) edge of the signal is used for triggering.
    '''
    '''Valid Values:
    '''RSNRPZ_SLOPE_POSITIVE (0) - Positive (Default Value)
    '''RSNRPZ_SLOPE_NEGATIVE (1) - Negative
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function trigger_setSlope(ByVal Channel As Integer, ByVal Trigger_Slope As Integer) As Integer
        Dim pInvokeResult As Integer = PInvoke.trigger_setSlope(Me._handle, Channel, Trigger_Slope)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function reads whether the rising (POSitive) or the falling (NEGative) edge of the signal is used for triggering.
    '''
    '''Remote-control command(s):
    '''TRIG:SLOP?
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Trigger_Slope">
    '''This control returns whether the rising (POSitive) or the falling (NEGative) edge of the signal is used for triggering.
    '''
    '''Valid Values:
    '''RSNRPZ_SLOPE_POSITIVE (0) - Positive
    '''RSNRPZ_SLOPE_NEGATIVE (1) - Negative
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function trigger_getSlope(ByVal Channel As Integer, ByRef Trigger_Slope As Integer) As Integer
        Dim pInvokeResult As Integer = PInvoke.trigger_getSlope(Me._handle, Channel, Trigger_Slope)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function sets the trigger signal source for the WAIT_FOR_TRG state.
    '''
    '''Remote-control command(s):
    '''TRIG:SOUR BUS | EXT | HOLD | IMM | INT
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Trigger_Source">
    '''This control selects the trigger signal source for the WAIT_FOR_TRG state.
    '''
    '''Valid Values:
    '''RSNRPZ_TRIGGER_SOURCE_BUS       (0) - Bus 
    '''RSNRPZ_TRIGGER_SOURCE_EXTERNAL  (1) - External
    '''RSNRPZ_TRIGGER_SOURCE_HOLD      (2) - Hold
    '''RSNRPZ_TRIGGER_SOURCE_IMMEDIATE (3) - Immediate (Default Value)
    '''RSNRPZ_TRIGGER_SOURCE_INTERNAL  (4) - Internal
    '''
    '''Notes:
    '''(1) Bus: The trigger event is initiated by TRIG:IMM or *TRG. In this case, the setting for Trigger Slope is meaningless.
    '''
    '''(2) External: Triggering is performed with an external signal applied to the trigger connector. The Trigger Slope setting determines whether the rising or the falling edge of the signal is to be used for triggering. Waiting for a trigger event can be skipped by Immediate Trigger.
    '''
    '''(3) Hold: A measurement can only be triggered when Immediate Trigger is executed.
    '''
    '''(4) Immediate: The sensor does not remain in the WAIT_FOR_TRG state but immediately changes to the MEASURING state.
    '''
    '''(5) Internal: The sensor determines the trigger time by means of the signal to be measured. When this signal exceeds the power set by Trigger Level, the measurement is started after the time set by Trigger Delay. Similar to External Trigger, waiting for a trigger event can also be skipped by Immediate Trigger.
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function trigger_setSource(ByVal Channel As Integer, ByVal Trigger_Source As Integer) As Integer
        Dim pInvokeResult As Integer = PInvoke.trigger_setSource(Me._handle, Channel, Trigger_Source)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function gets the trigger signal source for the WAIT_FOR_TRG state.
    '''
    '''Remote-control command(s):
    '''TRIG:SOUR?
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Trigger_Source">
    '''This control returns the trigger signal source for the WAIT_FOR_TRG state.
    '''
    '''Valid Values:
    '''RSNRPZ_TRIGGER_SOURCE_BUS (0) - Bus
    '''RSNRPZ_TRIGGER_SOURCE_EXTERNAL (1) - External
    '''RSNRPZ_TRIGGER_SOURCE_HOLD (2) - Hold
    '''RSNRPZ_TRIGGER_SOURCE_IMMEDIATE (3) - Immediate
    '''RSNRPZ_TRIGGER_SOURCE_INTERNAL (4) - Internal
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function trigger_getSource(ByVal Channel As Integer, ByRef Trigger_Source As Integer) As Integer
        Dim pInvokeResult As Integer = PInvoke.trigger_getSource(Me._handle, Channel, Trigger_Source)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function defines the dropout time value. With a positive (negative) trigger slope, the dropout time is the minimum time for which the signal must be below (above) the power level defined by rsnrpz_trigger_setLevel and rsnrpz_trigger_setHysteresis before triggering can occur again. As with the Holdoff parameter, unwanted trigger events can be excluded. The set dropout time only affects the internal trigger source.
    '''The dropout time parameter is useful when dealing with, for example, GSM signals with several active slots.
    '''
    '''Remote-control command(s):
    '''TRIGger:DTIMe
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Dropout_Time">
    '''This control defines the dropout time value.
    '''
    '''Valid Range:
    '''0.0 to 10.0 s
    '''
    '''Default Value:
    '''0.0 s
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function trigger_setDropoutTime(ByVal Channel As Integer, ByVal Dropout_Time As Double) As Integer
        Dim pInvokeResult As Integer = PInvoke.trigger_setDropoutTime(Me._handle, Channel, Dropout_Time)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function queries the dropout time value.
    '''
    '''Remote-control command(s):
    '''TRIGger:DTIMe?
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Dropout_Time">
    '''This control returns the dropout time value.
    '''
    '''Valid Range:
    '''0.0 to 10.0 s
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function trigger_getDropoutTime(ByVal Channel As Integer, ByRef Dropout_Time As Double) As Integer
        Dim pInvokeResult As Integer = PInvoke.trigger_getDropoutTime(Me._handle, Channel, Dropout_Time)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function can be used to configure an R&amp;S NRP-Z81 power sensor as the trigger master, enabling it to output a digital trigger signal in sync with its own trigger event. This makes it possible to synchronize several sensors (see rsnrpz_trigger_setSyncState) and to perform measurements in sync with a signal at very low power, which normally would not allow signal triggering. The trigger signal which is output has a length of 1Rs and the positive slope coincides with the physical trigger point. At present, it can be distributed to other R&amp;S NRP-Zxx sensors only via the R&amp;S NRP base unit and not via the R&amp;S NRP-Z3/-Z4 interface adapter.
    '''Generally, the trigger master is set to internal triggering (signal triggering) (the BUS and IMMEDIATE settings can also be used); the sensors acting as trigger slaves must be set to external triggering and positive trigger slope.
    '''With the R&amp;S NRP-Z81 power sensor, digital trigger signals are sent and received via a single differential line pair, the trigger bus. Only one instrument on the trigger bus can act as the trigger master. If the application is time-critical, the trigger-signal delay from the master to a slave must be taken into account.
    '''
    '''Remote-control command(s):
    '''TRIG:MAST:STAT ON | OFF
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="State">
    '''This control enables or disables trigger master.
    '''
    '''Valid Values:
    '''VI_TRUE  (1) - On
    '''VI_FALSE (0) - Off (Default Value)
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function trigger_setMasterState(ByVal Channel As Integer, ByVal State As Boolean) As Integer
        Dim pInvokeResult As Integer = PInvoke.trigger_setMasterState(Me._handle, Channel, System.Convert.ToUInt16(State))
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function queries state of trigger master.
    '''
    '''Remote-control command(s):
    '''TRIG:MAST:STAT?
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="State">
    '''This control returns state of trigger master.
    '''
    '''Valid Values:
    '''VI_TRUE  (1) - On
    '''VI_FALSE (0) - Off
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function trigger_getMasterState(ByVal Channel As Integer, ByRef State As Boolean) As Integer
        Dim StateAsUShort As UShort
        Dim pInvokeResult As Integer = PInvoke.trigger_getMasterState(Me._handle, Channel, StateAsUShort)
        State = System.Convert.ToBoolean(StateAsUShort)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function can be used to synchronize the sensors connected to the trigger bus. Synchronization is achieved by enabling the
    '''trigger signal only when all the sensors are in the WAIT_FOR_TRIGGER state (wired-OR). This ensures that the measurements are started simultaneously and also that repetitions due to averaging start at the same time. It must be ensured that the number of repetitions is the same for all the sensors involved in the measurement. Otherwise, the trigger bus will be blocked by any sensor that has completed its measurements before the others and has returned to the IDLE state.
    '''
    '''Remote-control command(s):
    '''TRIG:SYNC:STAT ON | OFF
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="State">
    '''This control enables or disables sensor synchronization.
    '''
    '''Valid Values:
    '''VI_TRUE  (1) - On
    '''VI_FALSE (0) - Off (Default Value)
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function trigger_setSyncState(ByVal Channel As Integer, ByVal State As Boolean) As Integer
        Dim pInvokeResult As Integer = PInvoke.trigger_setSyncState(Me._handle, Channel, System.Convert.ToUInt16(State))
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function queries state of sensor synchronization.
    '''
    '''Remote-control command(s):
    '''TRIG:SYNC:STAT?
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="State">
    '''This control returns state of sensor synchronization.
    '''
    '''Valid Values:
    '''VI_TRUE  (1) - On
    '''VI_FALSE (0) - Off
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function trigger_getSyncState(ByVal Channel As Integer, ByRef State As Boolean) As Integer
        Dim StateAsUShort As UShort
        Dim pInvokeResult As Integer = PInvoke.trigger_getSyncState(Me._handle, Channel, StateAsUShort)
        State = System.Convert.ToBoolean(StateAsUShort)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function returns selected information on a sensor.
    '''
    '''Remote-control command(s):
    '''SYST:INFO? &lt;Info Type&gt;
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Info_Type">
    '''This control specifies which info should be retrieved from the sensor.
    '''
    '''Valid Values:
    ''' "Manufacturer"
    ''' "Type"
    ''' "Stock Number"
    ''' "Serial"
    ''' "HWVersion"
    ''' "HWVariant"
    ''' "SW Build"
    ''' "Technology"
    ''' "Function"
    ''' "MinPower"
    ''' "MaxPower" 
    ''' "MinFreq"
    ''' "MaxFreq"
    ''' "Resolution"
    ''' "Impedance"
    ''' "Coupling"
    ''' "Cal. Abs."
    ''' "Cal. Refl."
    ''' "Cal. S-Para."
    ''' "Cal. Misc."
    ''' "Cal. Temp."
    ''' "Cal. Lin."
    ''' "SPD Mnemonic"
    '''
    '''Default Value:
    '''""
    '''</param>
    '''<param name="Array_Size">
    '''This control defines the size of array 'Info'.
    '''
    '''Valid Range:
    '''-
    '''
    '''Default Value:
    '''100
    '''</param>
    '''<param name="Info">
    '''This control returns requested informations from the sensor.
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function chan_info(ByVal Channel As Integer, ByVal Info_Type As String, ByVal Array_Size As Integer, ByVal Info As System.Text.StringBuilder) As Integer
        Dim pInvokeResult As Integer = PInvoke.chan_info(Me._handle, Channel, Info_Type, Array_Size, Info)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function returns specified parameter header which can be retrieved from selected sensor.
    '''
    '''Remote-control command(s):
    '''SYST:INFO?
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Parameter_Number">
    '''This control defines the position of parameter header to be retrieved.
    '''
    '''Valid Range:
    '''0 to (count of headers - 1)
    '''
    '''Default Value:
    '''0
    '''
    '''Notes:
    '''(1) Only Minimum value of the parameter is checked. Maximum value depends on sensor used and can be retrieved by function rsnrpz_chan_infosCount().
    '''</param>
    '''<param name="Array_Size">
    '''This control defines the size of array 'Header'.
    '''
    '''Valid Range:
    '''-
    '''
    '''Default Value:
    '''100
    '''</param>
    '''<param name="Header">
    '''This control returns specified parameter header.
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function chan_infoHeader(ByVal Channel As Integer, ByVal Parameter_Number As Integer, ByVal Array_Size As Integer, ByVal Header As System.Text.StringBuilder) As Integer
        Dim pInvokeResult As Integer = PInvoke.chan_infoHeader(Me._handle, Channel, Parameter_Number, Array_Size, Header)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function returns the number of info headers for selected channel.
    '''
    '''Remote-control command(s):
    '''SYST:INFO?
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Count">
    '''This control returns the number of available info headers for selected sensor.
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function chan_infosCount(ByVal Channel As Integer, ByRef Count As Integer) As Integer
        Dim pInvokeResult As Integer = PInvoke.chan_infosCount(Me._handle, Channel, Count)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function is checking whether the firmware-version of a sensor is reasonably actual.
    '''
    '''Remote-control command(s):
    '''SYST:INFO? "TYPE"
    '''SYST:INFO? "SW BUILD"
    '''</summary>
    '''<param name="Buffer_Size">
    '''Size of the character arrays which return current and
    '''required firmware version.
    '''(both char arrays should be same size and MUST be at least
    '''16 chars each)
    '''
    '''Valid Range:
    '''&gt;15
    '''
    '''Default Value:
    '''256
    '''</param>
    '''<param name="Firmware_Current">
    '''This control returns the character array for returning the firmware version of the sensor.
    '''
    '''Notes:
    '''(1) The array must contain at least 16 elements ViChar[16].
    '''</param>
    '''<param name="Firmware_Required_Minimum">
    '''This control returns the character array for returning the required miminum firmware version.
    '''
    '''Notes:
    '''(1) The array must contain at least 16 elements ViChar[16].
    '''</param>
    '''<param name="Firmware_Okay">
    '''This control returns 1 (TRUE) if sensor firmware is actual enough, 0 (FALSE) if firmware is out-dated. 
    '''This parameter can be set to NULL if you are not interested in the result of the firmware version check.
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function fw_version_check(ByVal Buffer_Size As Integer, ByVal Firmware_Current As System.Text.StringBuilder, ByVal Firmware_Required_Minimum As System.Text.StringBuilder, ByRef Firmware_Okay As Boolean) As Integer
        Dim Firmware_OkayAsUShort As UShort
        Dim pInvokeResult As Integer = PInvoke.fw_version_check(Me._handle, Buffer_Size, Firmware_Current, Firmware_Required_Minimum, Firmware_OkayAsUShort)
        Firmware_Okay = System.Convert.ToBoolean(Firmware_OkayAsUShort)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function sets status update time, which influences USB traffic during sensor's waiting for trigger.
    '''
    '''Note:
    '''
    '''1) This function is available only for NRP-Z81.
    '''
    '''Remote-control command(s):
    '''SYST:SUT
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Status_Update_Time">
    '''This control sets status update time, which influences USB traffic during sensor's waiting for trigger.
    '''
    '''Valid Range:
    '''0.0 to 10.0 s
    '''
    '''Default Value: 100.0e-4 s
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function system_setStatusUpdateTime(ByVal Channel As Integer, ByVal Status_Update_Time As Double) As Integer
        Dim pInvokeResult As Integer = PInvoke.system_setStatusUpdateTime(Me._handle, Channel, Status_Update_Time)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function gets status update time.
    '''
    '''Note:
    '''
    '''1) This function is available only for NRP-Z81.
    '''
    '''Remote-control command(s):
    '''SYST:SUT
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Status_Update_Time">
    '''This control returns status update time.
    '''
    '''Valid Range:
    '''0.0 to 10.0 s
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function system_getStatusUpdateTime(ByVal Channel As Integer, ByRef Status_Update_Time As Double) As Integer
        Dim pInvokeResult As Integer = PInvoke.system_getStatusUpdateTime(Me._handle, Channel, Status_Update_Time)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function sets result update time, which influences USB traffic if sensor is in continuous sweep mode.
    '''
    '''Note:
    '''
    '''1) This function is available only for NRP-Z81.
    '''
    '''Remote-control command(s):
    '''SYST:RUT
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Result_Update_Time">
    '''This control sets result update time, which influences USB traffic if sensor is in continuous sweep mode.
    '''
    '''Valid Range:
    '''0.0 to 10.0 s
    '''
    '''Default Value: 0.1 s
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function system_setResultUpdateTime(ByVal Channel As Integer, ByVal Result_Update_Time As Double) As Integer
        Dim pInvokeResult As Integer = PInvoke.system_setResultUpdateTime(Me._handle, Channel, Result_Update_Time)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function gets result update time.
    '''
    '''Note:
    '''
    '''1) This function is available only for NRP-Z81.
    '''
    '''Remote-control command(s):
    '''SYST:RUT
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Result_Update_Time">
    '''This control gets result update time.
    '''
    '''Valid Range:
    '''0.0 to 10.0 s
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function system_getResultUpdateTime(ByVal Channel As Integer, ByRef Result_Update_Time As Double) As Integer
        Dim pInvokeResult As Integer = PInvoke.system_getResultUpdateTime(Me._handle, Channel, Result_Update_Time)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function does internal test measurements with enabled and disabled heater and returns the power difference between both measurements.
    '''The result of this test is used to determine the long time drift of the power sensor.
    '''
    '''Note:
    '''
    '''1) This function is available only for NRP-Z56 and NRP-Z57.
    '''
    '''Remote-control command(s):
    '''CAL:TEST?
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Calib_Test_2">
    '''This control returns the power difference between internal test measurements with enabled and disabled heater.
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function calib_test(ByVal Channel As Integer, ByRef Calib_Test_2 As Double) As Integer
        Dim pInvokeResult As Integer = PInvoke.calib_test(Me._handle, Channel, Calib_Test_2)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function  first does an internal heater test with CAL:TEST and returns the relative deviation between the test result and the result that was measured in the calibration lab during sensor calibration.
    '''
    '''Note:
    '''
    '''1) This function is available only for NRP-Z56 and NRP-Z57.
    '''
    '''Remote-control command(s):
    '''CAL:TEST:DEV?
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Test_Deviation">
    '''This control returns the relative deviation between the test result and the result that was measured in the calibration lab during sensor calibration.
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function calib_getTestDeviation(ByVal Channel As Integer, ByRef Test_Deviation As Double) As Integer
        Dim pInvokeResult As Integer = PInvoke.calib_getTestDeviation(Me._handle, Channel, Test_Deviation)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function returns the heater test result that was measured in the calibration lab during sensor calibration.
    '''
    '''Note:
    '''
    '''1) This function is available only for NRP-Z56 and NRP-Z57.
    '''
    '''Remote-control command(s):
    '''CAL:TEST:REF?
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Test_Reference">
    '''This control returns the heater test result that was measured in the calibration lab during sensor calibration.
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function calib_getTestReference(ByVal Channel As Integer, ByRef Test_Reference As Double) As Integer
        Dim pInvokeResult As Integer = PInvoke.calib_getTestReference(Me._handle, Channel, Test_Reference)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function immediately sets selected sensor to the IDLE state. Measurements in progress are interrupted. If INIT:CONT ON is set, a new measurement is immediately started since the trigger system is not influenced.
    '''
    '''Remote-control command(s):
    '''ABOR
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function chan_abort(ByVal Channel As Integer) As Integer
        Dim pInvokeResult As Integer = PInvoke.chan_abort(Me._handle, Channel)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function starts a single-shot measurement on selected channel. The respective sensor goes to the INITIATED state. The command is completely executed when the sensor returns to the IDLE state. The command is ignored when the sensor is not in the IDLE state or when continuous measurements are selected (INIT:CONT ON). The command is only fully executed when the measurement is completed and the trigger system has again reached the IDLE state. INIT is the only remote control command that permits overlapping execution. Other commands can be received and processed while the command is being executed.
    '''
    '''Remote-control command(s):
    '''STAT:OPER:MEAS?
    '''INITiate[1..4]
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function chan_initiate(ByVal Channel As Integer) As Integer
        Dim pInvokeResult As Integer = PInvoke.chan_initiate(Me._handle, Channel)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function selects either single-shot or continuous (free-running) measurement cycles.
    '''
    '''Remote-control command(s):
    '''INIT:CONT ON | OFF
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Continuous_Initiate">
    '''This control enables or disables the continuous measurement mode.
    '''
    '''Valid Values:
    '''VI_TRUE  (1) - On
    '''VI_FALSE (0) - Off (Default Value)
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function chan_setInitContinuousEnabled(ByVal Channel As Integer, ByVal Continuous_Initiate As Boolean) As Integer
        Dim pInvokeResult As Integer = PInvoke.chan_setInitContinuousEnabled(Me._handle, Channel, System.Convert.ToUInt16(Continuous_Initiate))
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function returns whether single-shot or continuous (free-running) measurement is selected.
    '''
    '''Remote-control command(s):
    '''INIT:CONT?
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Continuous_Initiate">
    '''This control returns the state of continuous initiate.
    '''
    '''Valid Values:
    '''VI_TRUE  (1) - On
    '''VI_FALSE (0) - Off
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function chan_getInitContinuousEnabled(ByVal Channel As Integer, ByRef Continuous_Initiate As Boolean) As Integer
        Dim Continuous_InitiateAsUShort As UShort
        Dim pInvokeResult As Integer = PInvoke.chan_getInitContinuousEnabled(Me._handle, Channel, Continuous_InitiateAsUShort)
        Continuous_Initiate = System.Convert.ToBoolean(Continuous_InitiateAsUShort)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''From the point of view of the R&amp;S NRP basic unit, the sensors are stand-alone measuring devices. They communicate with the R&amp;S NRP via a command set complying with SCPI.
    '''This function prompts the basic unit to send an *RST to the respective sensor. Measurements in progress are interrupted.
    '''
    '''Remote-control command(s):
    '''SYSTem:SENSor[1..4]:RESet
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function chan_reset(ByVal Channel As Integer) As Integer
        Dim pInvokeResult As Integer = PInvoke.chan_reset(Me._handle, Channel)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''If the signal to be measured has modulation sections just above the video bandwidth of the sensor used, measurement errors might be caused due to aliasing effects. In this case, the sampling rate of the sensor can be set to a safe lower value (Sampling Frequency 2). However, the measurement time required to obtain noise-free results is extended compared to the normal sampling rate (Sampling Frequency 1).
    '''
    '''Note:
    '''
    '''1) This function is not available for NRP-Z51.
    '''
    '''Remote-control command(s):
    '''SENS:SAMP FREQ1 | FREQ2
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Sampling_Frequency">
    '''This control selects the sampling frequency.
    '''
    '''Valid Values:
    '''RSNRPZ_SAMPLING_FREQUENCY1 (1) - Sampling Frq 1 (High) (Default Value)
    '''RSNRPZ_SAMPLING_FREQUENCY2 (2) - Sampling Frq 2 (Low)
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function chan_setSamplingFrequency(ByVal Channel As Integer, ByVal Sampling_Frequency As Integer) As Integer
        Dim pInvokeResult As Integer = PInvoke.chan_setSamplingFrequency(Me._handle, Channel, Sampling_Frequency)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function returns the selected sampling frequency.
    '''
    '''Note:
    '''
    '''1) This function is not available for NRP-Z51.
    '''
    '''Remote-control command(s):
    '''SENS:SAMP?
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Sampling_Frequency">
    '''This control returns the selected sampling frequency.
    '''
    '''Valid Values:
    '''RSNRPZ_SAMPLING_FREQUENCY1 (1) - Sampling Frq 1 (High)
    '''RSNRPZ_SAMPLING_FREQUENCY2 (2) - Sampling Frq 2 (Low)
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function chan_getSamplingFrequency(ByVal Channel As Integer, ByRef Sampling_Frequency As Integer) As Integer
        Dim pInvokeResult As Integer = PInvoke.chan_getSamplingFrequency(Me._handle, Channel, Sampling_Frequency)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function starts zeroing of the selected sensor using the signal at the sensor input. Zeroing is an asynchronous operation which will require a couple of seconds. Therefore, after starting the function, the user should poll the current execution status by continuously calling rsnrpz_chan_isZeroComplete(). As soon as the zeroing has finished, the result of the operation can be queried by a call to rsnrpz_error_query(). See the example code below.
    '''
    '''Note: The sensor must be disconnected from all power sources. If the signal at the input considerably deviates from 0 W, the sensor aborts the zero calibration and raises an error condition. The rsnrpz driver queues the error for later retrieval by the rsnrpz_error_query() function.
    '''
    '''Example code
    '''
    '''bool Zero( ViSession lSesID )
    '''{
    '''  const int CH1 = 1;
    '''  ViStatus lStat = VI_SUCCESS;
    '''  ViBoolean bZeroComplete = VI_FALSE;
    '''  ViInt32 iErrorCode = VI_SUCCESS;
    '''  ViChar szErrorMsg[256];
    '''  /* Start zeroing the sensor */
    '''  lStat = rsnrpz_chan_zero( lSesID, CH1 );
    '''  if ( lStat != VI_SUCCESS )
    '''  {
    '''    fprintf( stderr, "Error 0x%08x in rsnrpz_chan_zero()", lStat );
    '''    return false;
    '''  }
    '''  while ( bZeroComplete == VI_FALSE )
    '''  {
    '''    lStat = rsnrpz_chan_isZeroComplete( lSesID, CH1, &amp;bZeroComplete );
    '''    if ( bZeroComplete )
    '''    {
    '''      rsnrpz_error_query( lSesID, &amp;iErrorCode, szErrorMsg );
    '''      fprintf( stderr, "Zero-Cal.: error %d, %s\n\n", iErrorCode, szErrorMsg );
    '''      break;
    '''    }
    '''    else 
    '''      SLEEP( 200 );
    '''  }
    '''  return iErrorCode == 0;
    '''}
    '''
    '''Remote-control command(s):
    '''STAT:OPER:MEAS?
    '''CAL:ZERO:AUTO ONCE
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function chan_zero(ByVal Channel As Integer) As Integer
        Dim pInvokeResult As Integer = PInvoke.chan_zero(Me._handle, Channel)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function performs zeroing using the signal at the sensor input. The sensor must be disconnected from all power sources. If the signal at the input considerably deviates from 0 W, an error message is issued and the function is aborted.
    '''
    '''Note(s):
    '''
    '''(1) This function is valid only for NRP-Z81.
    '''
    '''Remote-control command(s):
    '''CAL:ZERO:AUTO LFR | UFR
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Zeroing">
    '''This control selects type of advanced zeroing.
    '''
    '''Valid Values:
    '''RSNRPZ_ZERO_LFR (0) - Low Frequencies
    '''RSNRPZ_ZERO_UFR (1) - High Frequencies
    '''
    '''Default Value: RSNRPZ_ZERO_LFR (0)
    '''
    '''Note(s):
    '''
    '''(1) Low Frequencies - Does zeroing only for low frequencies (&lt; 500 MHZ)
    '''
    '''(2) High Frequencies - Does zeroing for higher Frequencies (&gt;= 500 MHz).
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function chan_zeroAdvanced(ByVal Channel As Integer, ByVal Zeroing As Integer) As Integer
        Dim pInvokeResult As Integer = PInvoke.chan_zeroAdvanced(Me._handle, Channel, Zeroing)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function should be used for polling whether a previously started zero calibration has already finished. Zero calibration is an asynchronous operation and may take some seconds until it completes. See the example code under rsnrpz_chan_zero() on how to conduct a sensor zeroing calibration.
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Zeroing_Complete">
    '''This control returns the state of the zeroing of the sensor.
    '''
    '''Valid Values:
    '''Complete (VI_TRUE)
    '''In Progress (VI_FALSE)
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function chan_isZeroComplete(ByVal Channel As Integer, ByRef Zeroing_Complete As Boolean) As Integer
        Dim Zeroing_CompleteAsUShort As UShort
        Dim pInvokeResult As Integer = PInvoke.chan_isZeroComplete(Me._handle, Channel, Zeroing_CompleteAsUShort)
        Zeroing_Complete = System.Convert.ToBoolean(Zeroing_CompleteAsUShort)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function returns the state of the measurement.
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Measurement_Complete">
    '''This control returns the state of the measurement.
    '''
    '''Valid Values:
    '''Complete (VI_TRUE)
    '''In Progress (VI_FALSE)
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function chan_isMeasurementComplete(ByVal Channel As Integer, ByRef Measurement_Complete As Boolean) As Integer
        Dim Measurement_CompleteAsUShort As UShort
        Dim pInvokeResult As Integer = PInvoke.chan_isMeasurementComplete(Me._handle, Channel, Measurement_CompleteAsUShort)
        Measurement_Complete = System.Convert.ToBoolean(Measurement_CompleteAsUShort)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function performs a sensor test and returns a list of strings separated by commas. The contents of this test protocol is sensor-specific. For its meaning, please refer to the sensor documentation.
    '''
    '''Remote-control command(s):
    '''TEST:SENS?
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Result">
    '''This control returns the result string of self-test.
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function chan_selfTest(ByVal Channel As Integer, ByVal Result As System.Text.StringBuilder) As Integer
        Dim pInvokeResult As Integer = PInvoke.chan_selfTest(Me._handle, Channel, Result)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function selects which measurement results are to be made available in the Trace mode.
    '''
    '''Note(s):
    '''
    '''(1) This functions is available only for NRP-Z81
    '''
    '''Remote-control command(s):
    '''SENSe:AUXiliary NONE | MINMAX | RNDMAX
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Auxiliary_Value">
    '''This control selects which measurement results are to be made available in the Trace mode.
    '''
    '''Valid Values:
    '''RSNRPZ_AUX_NONE   (0) - None
    '''RSNRPZ_AUX_MINMAX (1) - Min Max
    '''RSNRPZ_AUX_RNDMAX (2) - Rnd Max
    '''
    '''Note(s):
    '''
    '''(1) None - only the average power of the associated samples
    '''
    '''(2) Min Max - provides the maximum and minimum as well
    '''
    '''(3) Rnd Max - provides the maximum and a random sample.
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function chan_setAuxiliary(ByVal Channel As Integer, ByVal Auxiliary_Value As Integer) As Integer
        Dim pInvokeResult As Integer = PInvoke.chan_setAuxiliary(Me._handle, Channel, Auxiliary_Value)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function queries which measurement results are available in the Trace mode.
    '''
    '''Note(s):
    '''
    '''(1) This functions is available only for NRP-Z81
    '''
    '''Remote-control command(s):
    '''SENSe:AUXiliary?
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Auxiliary_Value">
    '''This control returns which measurement results are available in the Trace mode.
    '''
    '''Valid Values:
    '''RSNRPZ_AUX_NONE   (0) - None (Default Value)
    '''RSNRPZ_AUX_MINMAX (1) - Min Max
    '''RSNRPZ_AUX_RNDMAX (2) - Rnd Max
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function chan_getAuxiliary(ByVal Channel As Integer, ByRef Auxiliary_Value As Integer) As Integer
        Dim pInvokeResult As Integer = PInvoke.chan_getAuxiliary(Me._handle, Channel, Auxiliary_Value)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function initiates an acquisition on the channels that you specifies in channel parameter.  It then waits for the acquisition to complete, and returns the measurement for the channel you specify.
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Timeout__ms_">
    '''Pass the maximum length of time in which to allow the read measurement operation to complete.    
    '''
    '''If the operation does not complete within this time interval, the function returns the RSNRPZ_ERROR_MAX_TIME_EXCEEDED error code.  When this occurs, you can call rsnrpz_chan_abort to cancel the read measurement operation and return the sensor to the Idle state.
    '''
    '''Units:  milliseconds.  
    '''
    '''Defined Values:
    '''RSNRPZ_VAL_MAX_TIME_INFINITE
    '''RSNRPZ_VAL_MAX_TIME_IMMEDIATE
    '''
    '''Default Value: 5000 (ms)
    '''
    '''Notes:
    '''
    '''(1) The Maximum Time parameter applies only to this function.  It has no effect on other timeout parameters.
    '''</param>
    '''<param name="Measurement">
    '''Returns single measurement.
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function meass_readMeasurement(ByVal Channel As Integer, ByVal Timeout__ms_ As Integer, ByRef Measurement As Double) As Integer
        Dim pInvokeResult As Integer = PInvoke.meass_readMeasurement(Me._handle, Channel, Timeout__ms_, Measurement)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function returns the measurement the sensor acquires for the channel you specify.  The measurement is from an acquisition that you previously initiated.  
    '''
    '''You use the rsnrpz_chan_initiate function to start an acquisition on the channels that you specify. You use the rsnrpz_chan_isMeasurementComplete function to determine when the acquisition is complete.
    '''
    '''You can call the rsnrpz_meass_readMeasurement function instead of the rsnrpz_chan_initiate function.  The rsnrpz_meass_readMeasurement function starts an acquisition, waits for the acquisition to complete, and returns the measurement for the channel you specify.
    '''
    '''Note:
    '''
    '''1) If the acquisition has not be initialized or measurement is still in progress and value is not available, function returns an error( RSNRPZ_ERROR_MEAS_NOT_AVAILABLE ).
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Measurement">
    '''Returns single measurement.
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function meass_fetchMeasurement(ByVal Channel As Integer, ByRef Measurement As Double) As Integer
        Dim pInvokeResult As Integer = PInvoke.meass_fetchMeasurement(Me._handle, Channel, Measurement)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function initiates an acquisition on the channels that you specifies in channel parameter.  It then waits for the acquisition to complete, and returns the measurement for the channel you specify.
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Maximum_Time__ms_">
    '''Pass the maximum length of time in which to allow the read measurement operation to complete.    
    '''
    '''If the operation does not complete within this time interval, the function returns the RSNRPZ_ERROR_MAX_TIME_EXCEEDED error code.  When this occurs, you can call rsnrpz_chan_abort to cancel the read measurement operation and return the sensor to the Idle state.
    '''
    '''Units:  milliseconds.  
    '''
    '''Defined Values:
    '''RSNRPZ_VAL_MAX_TIME_INFINITE             RSNRPZ_VAL_MAX_TIME_IMMEDIATE           
    '''
    '''Default Value: 5000 (ms)
    '''
    '''Notes:
    '''
    '''(1) The Maximum Time parameter applies only to this function.  It has no effect on other timeout parameters.
    '''</param>
    '''<param name="Buffer_Size">
    '''Pass the number of elements in the Measurement Array parameter.
    '''
    '''Default Value: None
    '''
    '''</param>
    '''<param name="Measurement_Array">
    '''Returns the measurement buffer that the sensor acquires.  
    '''
    '''</param>
    '''<param name="Read_Count">
    '''Indicates the number of points the function places in the Measurement Array parameter.
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function meass_readBufferMeasurement(ByVal Channel As Integer, ByVal Maximum_Time__ms_ As Integer, ByVal Buffer_Size As Integer, ByVal Measurement_Array() As Double, ByRef Read_Count As Integer) As Integer
        Dim pInvokeResult As Integer = PInvoke.meass_readBufferMeasurement(Me._handle, Channel, Maximum_Time__ms_, Buffer_Size, Measurement_Array, Read_Count)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function returns the buffer measurement the sensor acquires for the channel you specify.  The measurement is from an acquisition that you previously initiated.  
    '''
    '''You use the rsnrpz_chan_initiate function to start an acquisition on the channels that you specify. You use the rsnrpz_chan_isMeasurementComplete function to determine when the acquisition is complete.
    '''
    '''You can call the rsnrpz_meas_readBufferMeasurement function instead of the rsnrpz_chan_initiate function.  The rsnrpz_meass_readBufferMeasurement function starts an acquisition, waits for the acquisition to complete, and returns the measurement for the channel you specify.
    '''
    '''Note:
    '''
    '''1) If the acquisition has not be initialized or measurement is still in progress and value is not available, function returns an error( RSNRPZ_ERROR_MEAS_NOT_AVAILABLE ).
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Array_Size">
    '''Pass the number of elements in the Measurement Array parameter.
    '''
    '''Default Value: None
    '''
    '''</param>
    '''<param name="Measurement_Array">
    '''Returns the measurement buffer that the sensor acquires.  
    '''
    '''</param>
    '''<param name="Read_Count">
    '''Indicates the number of points the function places in the Measurement Array parameter.
    '''
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function meass_fetchBufferMeasurement(ByVal Channel As Integer, ByVal Array_Size As Integer, ByVal Measurement_Array() As Double, ByRef Read_Count As Integer) As Integer
        Dim pInvokeResult As Integer = PInvoke.meass_fetchBufferMeasurement(Me._handle, Channel, Array_Size, Measurement_Array, Read_Count)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''Triggers a BUS event. If the sensor is in the WAIT_FOR_TRG state and the source for the trigger source is set to BUS, the sensor enters the MEASURING state. This function invalidates all current measuring results. A query of measurement data following this function will thus always return the measured value determined in response to this function.
    '''
    '''Remote-control command(s):
    '''*TRG
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function meass_sendSoftwareTrigger(ByVal Channel As Integer) As Integer
        Dim pInvokeResult As Integer = PInvoke.meass_sendSoftwareTrigger(Me._handle, Channel)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function initiates an acquisition on the channels that you specifies in channel parameter. It then waits for the acquisition to complete, and returns the auxiliary measurement for the channel you specify.
    '''
    '''Note(s):
    '''
    '''(1) If SENSE:AUX is set to None, this function returns error.
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Timeout__ms_">
    '''Pass the maximum length of time in which to allow the read measurement operation to complete.    
    '''
    '''If the operation does not complete within this time interval, the function returns the RSNRPZ_ERROR_MAX_TIME_EXCEEDED error code.  When this occurs, you can call rsnrpz_chan_abort to cancel the read measurement operation and return the sensor to the Idle state.
    '''
    '''Units:  milliseconds.  
    '''
    '''Defined Values:
    '''RSNRPZ_VAL_MAX_TIME_INFINITE
    '''RSNRPZ_VAL_MAX_TIME_IMMEDIATE
    '''
    '''Default Value: 5000 (ms)
    '''
    '''Notes:
    '''
    '''(1) The Maximum Time parameter applies only to this function.  It has no effect on other timeout parameters.
    '''</param>
    '''<param name="Measurement">
    '''Returns single measurement.
    '''</param>
    '''<param name="Aux_1">
    '''This control returns the first Auxiliary value.
    '''</param>
    '''<param name="Aux_2">
    '''This control returns the second Auxiliary value.
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function meass_readMeasurementAux(ByVal Channel As Integer, ByVal Timeout__ms_ As Integer, ByRef Measurement As Double, ByRef Aux_1 As Double, ByRef Aux_2 As Double) As Integer
        Dim pInvokeResult As Integer = PInvoke.meass_readMeasurementAux(Me._handle, Channel, Timeout__ms_, Measurement, Aux_1, Aux_2)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function returns the measurement the sensor acquires for the channel you specify.  The measurement is from an acquisition that you previously initiated.  
    '''
    '''You use the rsnrpz_chan_initiate function to start an acquisition on the channels that you specify. You use the rsnrpz_chan_isMeasurementComplete function to determine when the acquisition is complete.
    '''
    '''You can call the rsnrpz_meass_readMeasurement function instead of the rsnrpz_chan_initiate function.  The rsnrpz_meass_readMeasurement function starts an acquisition, waits for the acquisition to complete, and returns the measurement for the channel you specify.
    '''
    '''Note(s):
    '''
    '''1) If the acquisition has not be initialized or measurement is still in progress and value is not available, function returns an error( RSNRPZ_ERROR_MEAS_NOT_AVAILABLE ).
    '''
    '''2) If SENSE:AUX is set to None, this function returns error.
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Timeout__ms_">
    '''Pass the maximum length of time in which to allow the read measurement operation to complete.    
    '''
    '''If the operation does not complete within this time interval, the function returns the RSNRPZ_ERROR_MAX_TIME_EXCEEDED error code.  When this occurs, you can call rsnrpz_chan_abort to cancel the read measurement operation and return the sensor to the Idle state.
    '''
    '''Units:  milliseconds.  
    '''
    '''Defined Values:
    '''RSNRPZ_VAL_MAX_TIME_INFINITE
    '''RSNRPZ_VAL_MAX_TIME_IMMEDIATE
    '''
    '''Default Value: 5000 (ms)
    '''
    '''Notes:
    '''
    '''(1) The Maximum Time parameter applies only to this function.  It has no effect on other timeout parameters.
    '''</param>
    '''<param name="Measurement">
    '''Returns single measurement.
    '''</param>
    '''<param name="Aux_1">
    '''This control returns the first Auxiliary value.
    '''</param>
    '''<param name="Aux_2">
    '''This control returns the second Auxiliary value.
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function meass_fetchMeasurementAux(ByVal Channel As Integer, ByVal Timeout__ms_ As Integer, ByRef Measurement As Double, ByRef Aux_1 As Double, ByRef Aux_2 As Double) As Integer
        Dim pInvokeResult As Integer = PInvoke.meass_fetchMeasurementAux(Me._handle, Channel, Timeout__ms_, Measurement, Aux_1, Aux_2)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function initiates an acquisition on the channels that you specifies in channel parameter.  It then waits for the acquisition to complete, and returns the measurement for the channel you specify.
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Maximum_Time__ms_">
    '''Pass the maximum length of time in which to allow the read measurement operation to complete.    
    '''
    '''If the operation does not complete within this time interval, the function returns the RSNRPZ_ERROR_MAX_TIME_EXCEEDED error code.  When this occurs, you can call rsnrpz_chan_abort to cancel the read measurement operation and return the sensor to the Idle state.
    '''
    '''Units:  milliseconds.  
    '''
    '''Defined Values:
    '''RSNRPZ_VAL_MAX_TIME_INFINITE             RSNRPZ_VAL_MAX_TIME_IMMEDIATE           
    '''
    '''Default Value: 5000 (ms)
    '''
    '''Notes:
    '''
    '''(1) The Maximum Time parameter applies only to this function.  It has no effect on other timeout parameters.
    '''</param>
    '''<param name="Buffer_Size">
    '''Pass the number of elements in the Measurement Array parameter.
    '''
    '''Default Value: None
    '''
    '''</param>
    '''<param name="Measurement_Array">
    '''Returns the measurement buffer that the sensor acquires.  
    '''
    '''</param>
    '''<param name="Aux_1_Array">
    '''Returns the Aux 1 buffer that the sensor acquires.  
    '''
    '''</param>
    '''<param name="Aux_2_Array">
    '''Returns the Aux 2 buffer that the sensor acquires.  
    '''
    '''</param>
    '''<param name="Read_Count">
    '''Indicates the number of points the function places in the Measurement Array parameter.
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function meass_readBufferMeasurementAux(ByVal Channel As Integer, ByVal Maximum_Time__ms_ As Integer, ByVal Buffer_Size As Integer, ByVal Measurement_Array() As Double, ByVal Aux_1_Array() As Double, ByVal Aux_2_Array() As Double, ByRef Read_Count As Integer) As Integer
        Dim pInvokeResult As Integer = PInvoke.meass_readBufferMeasurementAux(Me._handle, Channel, Maximum_Time__ms_, Buffer_Size, Measurement_Array, Aux_1_Array, Aux_2_Array, Read_Count)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function returns the buffer measurement the sensor acquires for the channel you specify.  The measurement is from an acquisition that you previously initiated.  
    '''
    '''You use the rsnrpz_chan_initiate function to start an acquisition on the channels that you specify. You use the rsnrpz_chan_isMeasurementComplete function to determine when the acquisition is complete.
    '''
    '''You can call the rsnrpz_meas_readBufferMeasurement function instead of the rsnrpz_chan_initiate function.  The rsnrpz_meass_readBufferMeasurement function starts an acquisition, waits for the acquisition to complete, and returns the measurement for the channel you specify.
    '''
    '''Note:
    '''
    '''1) If the acquisition has not be initialized or measurement is still in progress and value is not available, function returns an error( RSNRPZ_ERROR_MEAS_NOT_AVAILABLE ).
    '''
    '''2) If SENSE:AUX is set to None, this function returns error.
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Maximum_Time__ms_">
    '''Pass the maximum length of time in which to allow the read measurement operation to complete.    
    '''
    '''If the operation does not complete within this time interval, the function returns the RSNRPZ_ERROR_MAX_TIME_EXCEEDED error code.  When this occurs, you can call rsnrpz_chan_abort to cancel the read measurement operation and return the sensor to the Idle state.
    '''
    '''Units:  milliseconds.  
    '''
    '''Defined Values:
    '''RSNRPZ_VAL_MAX_TIME_INFINITE             RSNRPZ_VAL_MAX_TIME_IMMEDIATE           
    '''
    '''Default Value: 5000 (ms)
    '''
    '''Notes:
    '''
    '''(1) The Maximum Time parameter applies only to this function.  It has no effect on other timeout parameters.
    '''</param>
    '''<param name="Buffer_Size">
    '''Pass the number of elements in the Measurement Array parameter.
    '''
    '''Default Value: None
    '''
    '''</param>
    '''<param name="Measurement_Array">
    '''Returns the measurement buffer that the sensor acquires.  
    '''
    '''</param>
    '''<param name="Aux_1_Array">
    '''Returns the Aux 1 buffer that the sensor acquires.  
    '''
    '''</param>
    '''<param name="Aux_2_Array">
    '''Returns the Aux 2 buffer that the sensor acquires.  
    '''
    '''</param>
    '''<param name="Read_Count">
    '''Indicates the number of points the function places in the Measurement Array parameter.
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function meass_fetchBufferMeasurementAux(ByVal Channel As Integer, ByVal Maximum_Time__ms_ As Integer, ByVal Buffer_Size As Integer, ByVal Measurement_Array() As Double, ByVal Aux_1_Array() As Double, ByVal Aux_2_Array() As Double, ByRef Read_Count As Integer) As Integer
        Dim pInvokeResult As Integer = PInvoke.meass_fetchBufferMeasurementAux(Me._handle, Channel, Maximum_Time__ms_, Buffer_Size, Measurement_Array, Aux_1_Array, Aux_2_Array, Read_Count)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function resets the R&amp;S NRPZ registry to default values.
    '''</summary>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function status_preset() As Integer
        Dim pInvokeResult As Integer = PInvoke.status_preset(Me._handle)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function checks selected status register for bits defined in Bitmask and returns a logical OR of all defined bits.
    '''</summary>
    '''<param name="Status_Class">
    '''This control selects the status register.
    '''
    '''Valid Values:
    '''RSNRPZ_STATCLASS_D_CONN (1) - Sensor Connected 
    '''RSNRPZ_STATCLASS_D_ERR  (2) - Sensor Error
    '''RSNRPZ_STATCLASS_O_CAL  (3) - Operation Calibrating Status Register
    '''RSNRPZ_STATCLASS_O_MEAS (4) - Operation Measuring Status Register
    '''RSNRPZ_STATCLASS_O_TRIGGER (5) - Operation Trigger Status Register
    '''RSNRPZ_STATCLASS_O_SENSE (6) - Operation Sense Status Register
    '''RSNRPZ_STATCLASS_O_LOWER (7) - Operation Lower Limit Fail Status Register
    '''RSNRPZ_STATCLASS_O_UPPER (8) - Operation Upper Limit Fail Status Register
    '''RSNRPZ_STATCLASS_Q_POWER (9) - Power Part of Questionable Power Status Register
    '''RSNRPZ_STATCLASS_Q_WINDOW (10) - Questionable Window Status Register
    '''RSNRPZ_STATCLASS_Q_CAL (11) - Questionable Calibration Status Register
    '''RSNRPZ_STATCLASS_Q_ZER (12) - Zero Part of Questionable Power Status Register
    '''
    '''</param>
    '''<param name="Mask">
    '''This control defines the bit which should be checked in the specified Status Register.
    '''
    '''You can use following constant for checking operation and questionable registers. To check multiple bits, bitwise OR them together. For example, if you want check sensor on channel 1 and sensor on channel 2, then pass 
    '''RSNRPZ_SENSOR_01 | RSNRPZ_SENSOR_02
    '''
    '''Valid Values:
    '''constant               bit     value
    '''RSNRPZ_SENSOR_01        1       0x2
    '''RSNRPZ_SENSOR_02        2       0x4
    '''RSNRPZ_SENSOR_03        3       0x8
    '''RSNRPZ_SENSOR_04        4       0x10 
    '''RSNRPZ_SENSOR_05        5       0x20 
    '''RSNRPZ_SENSOR_06        6       0x40 
    '''RSNRPZ_SENSOR_07        7       0x80 
    '''RSNRPZ_SENSOR_08        8       0x100
    '''RSNRPZ_SENSOR_09        9       0x200
    '''RSNRPZ_SENSOR_10       10       0x400 
    '''RSNRPZ_SENSOR_11       11       0x800 
    '''RSNRPZ_SENSOR_12       12       0x1000
    '''RSNRPZ_SENSOR_13       13       0x2000 
    '''RSNRPZ_SENSOR_14       14       0x4000
    '''RSNRPZ_SENSOR_15       15       0x8000
    '''RSNRPZ_SENSOR_16       16       0x10000
    '''RSNRPZ_SENSOR_17       17       0x20000
    '''RSNRPZ_SENSOR_18       18       0x40000
    '''RSNRPZ_SENSOR_19       19       0x80000
    '''RSNRPZ_SENSOR_20       20       0x100000
    '''RSNRPZ_SENSOR_21       21       0x200000
    '''RSNRPZ_SENSOR_22       22       0x400000
    '''RSNRPZ_SENSOR_23       23       0x800000
    '''RSNRPZ_SENSOR_24       24       0x1000000
    '''RSNRPZ_SENSOR_25       25       0x2000000
    '''RSNRPZ_SENSOR_26       26       0x4000000
    '''RSNRPZ_SENSOR_27       27       0x8000000
    '''RSNRPZ_SENSOR_28       28       0x10000000
    '''RSNRPZ_SENSOR_29       29       0x20000000
    '''RSNRPZ_SENSOR_30       30       0x40000000
    '''RSNRPZ_SENSOR_31       31       0x80000000
    '''RSNRPZ_ALL_SENSORS    all       0xFFFFFFFE
    '''
    '''Default Value:
    '''RSNRPZ_ALL_SENSORS
    '''</param>
    '''<param name="State">
    '''This control returns the bitwise OR of all bits defined by the bitmask.
    '''
    '''Valid Values:
    '''VI_TRUE (1) - Logical True
    '''VI_FALSE (0) - Logical False
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function status_checkCondition(ByVal Status_Class As Integer, ByVal Mask As UInteger, ByRef State As Boolean) As Integer
        Dim StateAsUShort As UShort
        Dim pInvokeResult As Integer = PInvoke.status_checkCondition(Me._handle, Status_Class, Mask, StateAsUShort)
        State = System.Convert.ToBoolean(StateAsUShort)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function sets the PTransition and NTransition register of selected status register according to bitmask.
    '''</summary>
    '''<param name="Status_Class">
    '''This control selects the status register.
    '''
    '''Valid Values:
    '''RSNRPZ_STATCLASS_D_CONN (1) - Sensor Connected 
    '''RSNRPZ_STATCLASS_D_ERR  (2) - Sensor Error
    '''RSNRPZ_STATCLASS_O_CAL  (3) - Operation Calibrating Status Register
    '''RSNRPZ_STATCLASS_O_MEAS (4) - Operation Measuring Status Register
    '''RSNRPZ_STATCLASS_O_TRIGGER (5) - Operation Trigger Status Register
    '''RSNRPZ_STATCLASS_O_SENSE (6) - Operation Sense Status Register
    '''RSNRPZ_STATCLASS_O_LOWER (7) - Operation Lower Limit Fail Status Register
    '''RSNRPZ_STATCLASS_O_UPPER (8) - Operation Upper Limit Fail Status Register
    '''RSNRPZ_STATCLASS_Q_POWER (9) - Power Part of Questionable Power Status Register
    '''RSNRPZ_STATCLASS_Q_WINDOW (10) - Questionable Window Status Register
    '''RSNRPZ_STATCLASS_Q_CAL (11) - Questionable Calibration Status Register
    '''RSNRPZ_STATCLASS_Q_ZER (12) - Zero Part of Questionable Power Status Register
    '''
    '''Notes:
    '''(1) For meaning of each status register consult Operation Manual.
    '''</param>
    '''<param name="Mask">
    '''This control defines the bit which should be checked in the specified Status Register.
    '''
    '''You can use following constant for checking operation and questionable registers. To check multiple bits, bitwise OR them together. For example, if you want check sensor on channel 1 and sensor on channel 2, then pass 
    '''RSNRPZ_SENSOR_01 | RSNRPZ_SENSOR_02
    '''
    '''Valid Values:
    '''constant               bit     value
    '''RSNRPZ_SENSOR_01        1       0x2
    '''RSNRPZ_SENSOR_02        2       0x4
    '''RSNRPZ_SENSOR_03        3       0x8
    '''RSNRPZ_SENSOR_04        4       0x10 
    '''RSNRPZ_SENSOR_05        5       0x20 
    '''RSNRPZ_SENSOR_06        6       0x40 
    '''RSNRPZ_SENSOR_07        7       0x80 
    '''RSNRPZ_SENSOR_08        8       0x100
    '''RSNRPZ_SENSOR_09        9       0x200
    '''RSNRPZ_SENSOR_10       10       0x400 
    '''RSNRPZ_SENSOR_11       11       0x800 
    '''RSNRPZ_SENSOR_12       12       0x1000
    '''RSNRPZ_SENSOR_13       13       0x2000 
    '''RSNRPZ_SENSOR_14       14       0x4000
    '''RSNRPZ_SENSOR_15       15       0x8000
    '''RSNRPZ_SENSOR_16       16       0x10000
    '''RSNRPZ_SENSOR_17       17       0x20000
    '''RSNRPZ_SENSOR_18       18       0x40000
    '''RSNRPZ_SENSOR_19       19       0x80000
    '''RSNRPZ_SENSOR_20       20       0x100000
    '''RSNRPZ_SENSOR_21       21       0x200000
    '''RSNRPZ_SENSOR_22       22       0x400000
    '''RSNRPZ_SENSOR_23       23       0x800000
    '''RSNRPZ_SENSOR_24       24       0x1000000
    '''RSNRPZ_SENSOR_25       25       0x2000000
    '''RSNRPZ_SENSOR_26       26       0x4000000
    '''RSNRPZ_SENSOR_27       27       0x8000000
    '''RSNRPZ_SENSOR_28       28       0x10000000
    '''RSNRPZ_SENSOR_29       29       0x20000000
    '''RSNRPZ_SENSOR_30       30       0x40000000
    '''RSNRPZ_SENSOR_31       31       0x80000000
    '''RSNRPZ_ALL_SENSORS    all       0xFFFFFFFE
    '''
    '''Default Value:
    '''RSNRPZ_ALL_SENSORS
    '''</param>
    '''<param name="Direction">
    '''This control defines the direction of transition of the event.
    '''
    '''Valid Values:
    '''RSNRPZ_DIRECTION_NONE (0) - None Transition
    '''RSNRPZ_DIRECTION_PTR (1) - Positive Transition  (Default Value)
    '''RSNRPZ_DIRECTION_NTR (2) - Negative Transition
    '''RSNRPZ_DIRECTION_BOTH (3) - Both Transition
    '''
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function status_catchEvent(ByVal Status_Class As Integer, ByVal Mask As UInteger, ByVal Direction As Integer) As Integer
        Dim pInvokeResult As Integer = PInvoke.status_catchEvent(Me._handle, Status_Class, Mask, Direction)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function checks the selected status register for events specified by bitmask and sets returns their states. Finally all bits of shadow status register specified by Reset Action will be set to zero.
    '''
    '''</summary>
    '''<param name="Status_Class">
    '''This control selects the status register.
    '''
    '''Valid Values:
    '''RSNRPZ_STATCLASS_D_CONN (1) - Sensor Connected 
    '''RSNRPZ_STATCLASS_D_ERR  (2) - Sensor Error
    '''RSNRPZ_STATCLASS_O_CAL  (3) - Operation Calibrating Status Register
    '''RSNRPZ_STATCLASS_O_MEAS (4) - Operation Measuring Status Register
    '''RSNRPZ_STATCLASS_O_TRIGGER (5) - Operation Trigger Status Register
    '''RSNRPZ_STATCLASS_O_SENSE (6) - Operation Sense Status Register
    '''RSNRPZ_STATCLASS_O_LOWER (7) - Operation Lower Limit Fail Status Register
    '''RSNRPZ_STATCLASS_O_UPPER (8) - Operation Upper Limit Fail Status Register
    '''RSNRPZ_STATCLASS_Q_POWER (9) - Power Part of Questionable Power Status Register
    '''RSNRPZ_STATCLASS_Q_WINDOW (10) - Questionable Window Status Register
    '''RSNRPZ_STATCLASS_Q_CAL (11) - Questionable Calibration Status Register
    '''RSNRPZ_STATCLASS_Q_ZER (12) - Zero Part of Questionable Power Status Register
    '''
    '''Notes:
    '''(1) For meaning of each status register consult Operation Manual.
    '''</param>
    '''<param name="Mask">
    '''This control defines the bit which should be checked in the specified Status Register.
    '''
    '''You can use following constant for checking operation and questionable registers. To check multiple bits, bitwise OR them together. For example, if you want check sensor on channel 1 and sensor on channel 2, then pass 
    '''RSNRPZ_SENSOR_01 | RSNRPZ_SENSOR_02
    '''
    '''Valid Values:
    '''constant               bit     value
    '''RSNRPZ_SENSOR_01        1       0x2
    '''RSNRPZ_SENSOR_02        2       0x4
    '''RSNRPZ_SENSOR_03        3       0x8
    '''RSNRPZ_SENSOR_04        4       0x10 
    '''RSNRPZ_SENSOR_05        5       0x20 
    '''RSNRPZ_SENSOR_06        6       0x40 
    '''RSNRPZ_SENSOR_07        7       0x80 
    '''RSNRPZ_SENSOR_08        8       0x100
    '''RSNRPZ_SENSOR_09        9       0x200
    '''RSNRPZ_SENSOR_10       10       0x400 
    '''RSNRPZ_SENSOR_11       11       0x800 
    '''RSNRPZ_SENSOR_12       12       0x1000
    '''RSNRPZ_SENSOR_13       13       0x2000 
    '''RSNRPZ_SENSOR_14       14       0x4000
    '''RSNRPZ_SENSOR_15       15       0x8000
    '''RSNRPZ_SENSOR_16       16       0x10000
    '''RSNRPZ_SENSOR_17       17       0x20000
    '''RSNRPZ_SENSOR_18       18       0x40000
    '''RSNRPZ_SENSOR_19       19       0x80000
    '''RSNRPZ_SENSOR_20       20       0x100000
    '''RSNRPZ_SENSOR_21       21       0x200000
    '''RSNRPZ_SENSOR_22       22       0x400000
    '''RSNRPZ_SENSOR_23       23       0x800000
    '''RSNRPZ_SENSOR_24       24       0x1000000
    '''RSNRPZ_SENSOR_25       25       0x2000000
    '''RSNRPZ_SENSOR_26       26       0x4000000
    '''RSNRPZ_SENSOR_27       27       0x8000000
    '''RSNRPZ_SENSOR_28       28       0x10000000
    '''RSNRPZ_SENSOR_29       29       0x20000000
    '''RSNRPZ_SENSOR_30       30       0x40000000
    '''RSNRPZ_SENSOR_31       31       0x80000000
    '''RSNRPZ_ALL_SENSORS    all       0xFFFFFFFE
    '''
    '''Default Value:
    '''RSNRPZ_ALL_SENSORS
    '''</param>
    '''<param name="Reset_Mask">
    '''This control defines which bits of the shadow status register will reset to zero when finishing the function.
    '''
    '''You can use following constant for reset operation. To reset multiple bits, bitwise OR them together. For example, if you want reset only bits corresponding with sensor on channel 1 and sensor on channel 2, then pass 
    '''RSNRPZ_SENSOR_01 | RSNRPZ_SENSOR_02
    '''
    '''Valid Values:
    '''constant               bit     value
    '''RSNRPZ_SENSOR_01        1       0x2
    '''RSNRPZ_SENSOR_02        2       0x4
    '''RSNRPZ_SENSOR_03        3       0x8
    '''RSNRPZ_SENSOR_04        4       0x10 
    '''RSNRPZ_SENSOR_05        5       0x20 
    '''RSNRPZ_SENSOR_06        6       0x40 
    '''RSNRPZ_SENSOR_07        7       0x80 
    '''RSNRPZ_SENSOR_08        8       0x100
    '''RSNRPZ_SENSOR_09        9       0x200
    '''RSNRPZ_SENSOR_10       10       0x400 
    '''RSNRPZ_SENSOR_11       11       0x800 
    '''RSNRPZ_SENSOR_12       12       0x1000
    '''RSNRPZ_SENSOR_13       13       0x2000 
    '''RSNRPZ_SENSOR_14       14       0x4000
    '''RSNRPZ_SENSOR_15       15       0x8000
    '''RSNRPZ_SENSOR_16       16       0x10000
    '''RSNRPZ_SENSOR_17       17       0x20000
    '''RSNRPZ_SENSOR_18       18       0x40000
    '''RSNRPZ_SENSOR_19       19       0x80000
    '''RSNRPZ_SENSOR_20       20       0x100000
    '''RSNRPZ_SENSOR_21       21       0x200000
    '''RSNRPZ_SENSOR_22       22       0x400000
    '''RSNRPZ_SENSOR_23       23       0x800000
    '''RSNRPZ_SENSOR_24       24       0x1000000
    '''RSNRPZ_SENSOR_25       25       0x2000000
    '''RSNRPZ_SENSOR_26       26       0x4000000
    '''RSNRPZ_SENSOR_27       27       0x8000000
    '''RSNRPZ_SENSOR_28       28       0x10000000
    '''RSNRPZ_SENSOR_29       29       0x20000000
    '''RSNRPZ_SENSOR_30       30       0x40000000
    '''RSNRPZ_SENSOR_31       31       0x80000000
    '''RSNRPZ_ALL_SENSORS    all       0xFFFFFFFE
    '''
    '''Default Value:
    '''RSNRPZ_ALL_SENSORS
    '''</param>
    '''<param name="Events">
    '''This control returns the bitwise OR of all bits defined by the bitmask.
    '''
    '''Valid Values:
    '''VI_TRUE (1) - Logical True
    '''VI_FALSE (0) - Logical False
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function status_checkEvent(ByVal Status_Class As Integer, ByVal Mask As UInteger, ByVal Reset_Mask As UInteger, ByRef Events As Boolean) As Integer
        Dim EventsAsUShort As UShort
        Dim pInvokeResult As Integer = PInvoke.status_checkEvent(Me._handle, Status_Class, Mask, Reset_Mask, EventsAsUShort)
        Events = System.Convert.ToBoolean(EventsAsUShort)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function enables events defined by Bitmask in enable register respective to the selected status register.
    '''</summary>
    '''<param name="Status_Class">
    '''This control selects the status register.
    '''
    '''Valid Values:
    '''RSNRPZ_STATCLASS_D_CONN (1) - Sensor Connected 
    '''RSNRPZ_STATCLASS_D_ERR  (2) - Sensor Error
    '''RSNRPZ_STATCLASS_O_CAL  (3) - Operation Calibrating Status Register
    '''RSNRPZ_STATCLASS_O_MEAS (4) - Operation Measuring Status Register
    '''RSNRPZ_STATCLASS_O_TRIGGER (5) - Operation Trigger Status Register
    '''RSNRPZ_STATCLASS_O_SENSE (6) - Operation Sense Status Register
    '''RSNRPZ_STATCLASS_O_LOWER (7) - Operation Lower Limit Fail Status Register
    '''RSNRPZ_STATCLASS_O_UPPER (8) - Operation Upper Limit Fail Status Register
    '''RSNRPZ_STATCLASS_Q_POWER (9) - Power Part of Questionable Power Status Register
    '''RSNRPZ_STATCLASS_Q_WINDOW (10) - Questionable Window Status Register
    '''RSNRPZ_STATCLASS_Q_CAL (11) - Questionable Calibration Status Register
    '''RSNRPZ_STATCLASS_Q_ZER (12) - Zero Part of Questionable Power Status Register
    '''
    '''Notes:
    '''(1) For meaning of each status register consult Operation Manual.
    '''</param>
    '''<param name="Mask">
    '''This control defines the bits(channels) which should be set to one and will generate SRQ.
    '''
    '''You can use following constant for enabling SRQ for specified channels. To disable multiple channels, bitwise OR them together. For example, if you want enable SRQ for sensor on channel 1 and sensor on channel 2, then pass 
    '''RSNRPZ_SENSOR_01 | RSNRPZ_SENSOR_02.
    '''
    '''Valid Values:
    '''constant               bit     value
    '''RSNRPZ_SENSOR_01        1       0x2
    '''RSNRPZ_SENSOR_02        2       0x4
    '''RSNRPZ_SENSOR_03        3       0x8
    '''RSNRPZ_SENSOR_04        4       0x10 
    '''RSNRPZ_SENSOR_05        5       0x20 
    '''RSNRPZ_SENSOR_06        6       0x40 
    '''RSNRPZ_SENSOR_07        7       0x80 
    '''RSNRPZ_SENSOR_08        8       0x100
    '''RSNRPZ_SENSOR_09        9       0x200
    '''RSNRPZ_SENSOR_10       10       0x400 
    '''RSNRPZ_SENSOR_11       11       0x800 
    '''RSNRPZ_SENSOR_12       12       0x1000
    '''RSNRPZ_SENSOR_13       13       0x2000 
    '''RSNRPZ_SENSOR_14       14       0x4000
    '''RSNRPZ_SENSOR_15       15       0x8000
    '''RSNRPZ_SENSOR_16       16       0x10000
    '''RSNRPZ_SENSOR_17       17       0x20000
    '''RSNRPZ_SENSOR_18       18       0x40000
    '''RSNRPZ_SENSOR_19       19       0x80000
    '''RSNRPZ_SENSOR_20       20       0x100000
    '''RSNRPZ_SENSOR_21       21       0x200000
    '''RSNRPZ_SENSOR_22       22       0x400000
    '''RSNRPZ_SENSOR_23       23       0x800000
    '''RSNRPZ_SENSOR_24       24       0x1000000
    '''RSNRPZ_SENSOR_25       25       0x2000000
    '''RSNRPZ_SENSOR_26       26       0x4000000
    '''RSNRPZ_SENSOR_27       27       0x8000000
    '''RSNRPZ_SENSOR_28       28       0x10000000
    '''RSNRPZ_SENSOR_29       29       0x20000000
    '''RSNRPZ_SENSOR_30       30       0x40000000
    '''RSNRPZ_SENSOR_31       31       0x80000000
    '''RSNRPZ_ALL_SENSORS    all       0xFFFFFFFE
    '''
    '''Default Value:
    '''RSNRPZ_ALL_SENSORS
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function status_enableEventNotification(ByVal Status_Class As Integer, ByVal Mask As UInteger) As Integer
        Dim pInvokeResult As Integer = PInvoke.status_enableEventNotification(Me._handle, Status_Class, Mask)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function disables events defined by Bitmask in enable register respective to the selected status register.
    '''</summary>
    '''<param name="Status_Class">
    '''This control selects the status register.
    '''
    '''Valid Values:
    '''RSNRPZ_STATCLASS_D_CONN (1) - Sensor Connected 
    '''RSNRPZ_STATCLASS_D_ERR  (2) - Sensor Error
    '''RSNRPZ_STATCLASS_O_CAL  (3) - Operation Calibrating Status Register
    '''RSNRPZ_STATCLASS_O_MEAS (4) - Operation Measuring Status Register
    '''RSNRPZ_STATCLASS_O_TRIGGER (5) - Operation Trigger Status Register
    '''RSNRPZ_STATCLASS_O_SENSE (6) - Operation Sense Status Register
    '''RSNRPZ_STATCLASS_O_LOWER (7) - Operation Lower Limit Fail Status Register
    '''RSNRPZ_STATCLASS_O_UPPER (8) - Operation Upper Limit Fail Status Register
    '''RSNRPZ_STATCLASS_Q_POWER (9) - Power Part of Questionable Power Status Register
    '''RSNRPZ_STATCLASS_Q_WINDOW (10) - Questionable Window Status Register
    '''RSNRPZ_STATCLASS_Q_CAL (11) - Questionable Calibration Status Register
    '''RSNRPZ_STATCLASS_Q_ZER (12) - Zero Part of Questionable Power Status Register
    '''
    '''Notes:
    '''(1) For meaning of each status register consult Operation Manual.
    '''</param>
    '''<param name="Mask">
    '''This control defines the bit which should be set to zero in the specified Enable Register.
    '''
    '''You can use following constant for disabling SRQ for specified channels. To disable multiple channels, bitwise OR them together. For example, if you want disable SRQ for sensor on channel 1 and sensor on channel 2, then pass 
    '''RSNRPZ_SENSOR_01 | RSNRPZ_SENSOR_02.
    '''
    '''Valid Values:
    '''constant               bit     value
    '''RSNRPZ_SENSOR_01        1       0x2
    '''RSNRPZ_SENSOR_02        2       0x4
    '''RSNRPZ_SENSOR_03        3       0x8
    '''RSNRPZ_SENSOR_04        4       0x10 
    '''RSNRPZ_SENSOR_05        5       0x20 
    '''RSNRPZ_SENSOR_06        6       0x40 
    '''RSNRPZ_SENSOR_07        7       0x80 
    '''RSNRPZ_SENSOR_08        8       0x100
    '''RSNRPZ_SENSOR_09        9       0x200
    '''RSNRPZ_SENSOR_10       10       0x400 
    '''RSNRPZ_SENSOR_11       11       0x800 
    '''RSNRPZ_SENSOR_12       12       0x1000
    '''RSNRPZ_SENSOR_13       13       0x2000 
    '''RSNRPZ_SENSOR_14       14       0x4000
    '''RSNRPZ_SENSOR_15       15       0x8000
    '''RSNRPZ_SENSOR_16       16       0x10000
    '''RSNRPZ_SENSOR_17       17       0x20000
    '''RSNRPZ_SENSOR_18       18       0x40000
    '''RSNRPZ_SENSOR_19       19       0x80000
    '''RSNRPZ_SENSOR_20       20       0x100000
    '''RSNRPZ_SENSOR_21       21       0x200000
    '''RSNRPZ_SENSOR_22       22       0x400000
    '''RSNRPZ_SENSOR_23       23       0x800000
    '''RSNRPZ_SENSOR_24       24       0x1000000
    '''RSNRPZ_SENSOR_25       25       0x2000000
    '''RSNRPZ_SENSOR_26       26       0x4000000
    '''RSNRPZ_SENSOR_27       27       0x8000000
    '''RSNRPZ_SENSOR_28       28       0x10000000
    '''RSNRPZ_SENSOR_29       29       0x20000000
    '''RSNRPZ_SENSOR_30       30       0x40000000
    '''RSNRPZ_SENSOR_31       31       0x80000000
    '''RSNRPZ_ALL_SENSORS    all       0xFFFFFFFE
    '''
    '''Default Value:
    '''RSNRPZ_ALL_SENSORS
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function status_disableEventNotification(ByVal Status_Class As Integer, ByVal Mask As UInteger) As Integer
        Dim pInvokeResult As Integer = PInvoke.status_disableEventNotification(Me._handle, Status_Class, Mask)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function returns the Nrp low level driver state
    '''</summary>
    '''<param name="Driver_State">
    '''This control returns the Nrp low level driver state.
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This driver defines the following status codes:
    '''          
    '''Status    Description
    '''-------------------------------------------------
    '''3FFF0085  Unknown status code - VI_WARN_UNKNOWN_STATUS
    '''          
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function status_driverOpenState(ByRef Driver_State As Boolean) As Integer
        Dim Driver_StateAsUShort As UShort
        Dim pInvokeResult As Integer = PInvoke.status_driverOpenState(Me._handle, Driver_StateAsUShort)
        Driver_State = System.Convert.ToBoolean(Driver_StateAsUShort)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function registers message, which will be send to specified window, when SRQ is occured.
    '''</summary>
    '''<param name="Window_Handle">
    '''Handle to the window whose window procedure is to receive the message. If the parameter is set to 0 (NULL), the message is disabled.
    '''</param>
    '''<param name="Message_ID">
    '''Specifies the message to be posted.  If the message ID is set to 0, message will be not posted.
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function status_registerWindowMessage(ByVal Window_Handle As UInteger, ByVal Message_ID As UInteger) As Integer
        Dim pInvokeResult As Integer = PInvoke.status_registerWindowMessage(Me._handle, Window_Handle, Message_ID)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function initiates a temperature measurement of the sensor and reads the temperature value from the instrument.
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Temperature">
    '''This control returns the temperature value from the instrument.
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This driver defines the following status codes:
    '''          
    '''Status    Description
    '''-------------------------------------------------
    '''3FFF0085  Unknown status code - VI_WARN_UNKNOWN_STATUS
    '''          
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function service_getDetectorTemperature(ByVal Channel As Integer, ByRef Temperature As Double) As Integer
        Dim pInvokeResult As Integer = PInvoke.service_getDetectorTemperature(Me._handle, Channel, Temperature)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function sets the number of simulation pairs count-value.
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Block_Count">
    '''This control sets the number of simulation pairs count-value.
    '''
    '''Valid Values: not checked
    '''
    '''Default Value: 100
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This driver defines the following status codes:
    '''          
    '''Status    Description
    '''-------------------------------------------------
    '''3FFF0085  Unknown status code - VI_WARN_UNKNOWN_STATUS
    '''          
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function service_startSimulation(ByVal Channel As Integer, ByVal Block_Count As Integer) As Integer
        Dim pInvokeResult As Integer = PInvoke.service_startSimulation(Me._handle, Channel, Block_Count)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function sets the values which will be simulated. Right before calling this function must be called function rsnrpz_service_startSimulation!
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Value_Count">
    '''This control sets the value count. The amount of values is equal to Block Count set with function rsnrpz_service_startSimulation.
    '''</param>
    '''<param name="Values">
    '''This control sets the values which will be simulated. The amount of values is equal to Block Count set with function rsnrpz_service_startSimulation.
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This driver defines the following status codes:
    '''          
    '''Status    Description
    '''-------------------------------------------------
    '''3FFF0085  Unknown status code - VI_WARN_UNKNOWN_STATUS
    '''          
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function service_setSimulationValues(ByVal Channel As Integer, ByVal Value_Count() As Integer, ByVal Values() As Double) As Integer
        Dim pInvokeResult As Integer = PInvoke.service_setSimulationValues(Me._handle, Channel, Value_Count, Values)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function stops the simulation by setting the count-value pairs to zero.
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This driver defines the following status codes:
    '''          
    '''Status    Description
    '''-------------------------------------------------
    '''3FFF0085  Unknown status code - VI_WARN_UNKNOWN_STATUS
    '''          
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function service_stopSimulation(ByVal Channel As Integer) As Integer
        Dim pInvokeResult As Integer = PInvoke.service_stopSimulation(Me._handle, Channel)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function switches state checking of the instrument (reading of the Standard Event Register and checking it for error) status subsystem. Driver functions are using state checking which is by default enabled.
    '''
    '''Note:
    '''
    '''(1) In debug mode enable state checking.
    '''
    '''(2) For better bus throughput and instruments performance disable state checking.
    '''
    '''(3) When state checking is disabled driver does not check if correct instrument model or option is used with each of the functions. This might cause unexpected behaviour of the instrument.
    '''
    '''</summary>
    '''<param name="State_Checking">
    '''This control switches instrument state checking On or Off.
    '''
    '''Valid Range:
    '''VI_FALSE (0) - Off
    '''VI_TRUE  (1) - On (Default Value)
    '''
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This driver defines the following status codes:
    '''          
    '''Status    Description
    '''-------------------------------------------------
    '''BFFC0002  Parameter 2 (State Checking) out of range.
    '''          
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function errorCheckState(ByVal State_Checking As Boolean) As Integer
        Dim pInvokeResult As Integer = PInvoke.errorCheckState(Me._handle, System.Convert.ToUInt16(State_Checking))
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function resets the instrument to a known state and sends initialization commands to the instrument that set any necessary programmatic variables such as Headers Off, Short Command form, and Data Transfer Binary to the state necessary for the operation of the instrument driver.
    '''
    '''</summary>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors 
    '''</returns>
    Public Function reset() As Integer
        Dim pInvokeResult As Integer = PInvoke.reset(Me._handle)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function runs the instrument's self test routine and returns the test result(s).
    '''
    '''</summary>
    '''<param name="Self_Test_Result">
    '''This control contains the value returned from the instrument self test.  Zero means success.  For any other code, see the device's operator's manual.
    '''
    '''</param>
    '''<param name="Self_Test_Message">
    '''This control contains the string returned from the self test. See the device's operation manual for an explanation of the string's contents.
    '''
    '''Notes:
    '''
    '''(1) The array must contain at least 256 elements ViChar[256].
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This driver defines the following status codes:
    '''          
    '''Status    Description
    '''-------------------------------------------------
    '''BFFC0002  Parameter 2 (Self-Test Result) NULL pointer.
    '''          
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function self_test(ByRef Self_Test_Result As Short, ByVal Self_Test_Message As System.Text.StringBuilder) As Integer
        Dim pInvokeResult As Integer = PInvoke.self_test(Me._handle, Self_Test_Result, Self_Test_Message)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function reads an error code from the instrument's error queue.
    '''
    '''</summary>
    '''<param name="Error_Code">
    '''This control returns the error code read from the instrument's error queue.
    '''
    '''</param>
    '''<param name="Error_Message">
    '''This control returns the error message string read from the instrument's error message queue.
    '''
    '''Notes:
    '''
    '''(1) The array must contain at least 256 elements ViChar[256].
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This driver defines the following status codes:
    '''          
    '''Status    Description
    '''-------------------------------------------------
    '''BFFC0002  Parameter 2 (Error Code) NULL pointer.
    '''BFFC0003  Parameter 3 (Error Message) NULL pointer.
    '''          
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function error_query(ByRef Error_Code As Integer, ByVal Error_Message As System.Text.StringBuilder) As Integer
        Dim pInvokeResult As Integer = PInvoke.error_query(Me._handle, Error_Code, Error_Message)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function returns the revision numbers of the instrument driver and instrument firmware, and tells the user with which  instrument firmware this revision of the driver is compatible. 
    '''
    '''</summary>
    '''<param name="Instrument_Driver_Revision">
    '''This control returns the Instrument Driver Software Revision.
    '''
    '''Notes:
    '''
    '''(1) The array must contain at least 256 elements ViChar[256].
    '''</param>
    '''<param name="Firmware_Revision">
    '''This control returns the Instrument Firmware Revision.
    '''
    '''Notes:
    '''
    '''(1) Because instrument does not support firmware revision the array is set to empty string or ignored if used VI_NULL.
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This driver defines the following status codes:
    '''          
    '''Status    Description
    '''-------------------------------------------------
    '''3FFC0105  Revision query not supported - VI_WARN_NSUP_REV_QUERY
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function revision_query(ByVal Instrument_Driver_Revision As System.Text.StringBuilder, ByVal Firmware_Revision As System.Text.StringBuilder) As Integer
        Dim pInvokeResult As Integer = PInvoke.revision_query(Me._handle, Instrument_Driver_Revision, Firmware_Revision)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function returns the number of currently connected sensors
    '''</summary>
    '''<param name="Sensor_Count">
    '''This control returns the number of currently connected sensors
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This driver defines the following status codes:
    '''          
    '''Status    Description
    '''-------------------------------------------------
    '''3FFF0085  Unknown status code - VI_WARN_UNKNOWN_STATUS
    '''          
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function GetSensorCount(ByRef Sensor_Count As Integer) As Integer
        Dim pInvokeResult As Integer = PInvoke.GetSensorCount(Me._handle, Sensor_Count)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function returns the name/descriptor of a connected sensor.
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Sensor_Name">
    '''This control returns selected sensor's name.
    '''
    '''Note(s):
    '''
    '''(1) The array has to have allocated at least 128 elements.
    '''</param>
    '''<param name="Sensor_Type">
    '''This control returns selected sensor's type.
    '''
    '''Note(s):
    '''
    '''(1) The array has to have allocated at least 128 elements.
    '''</param>
    '''<param name="Sensor_Serial">
    '''This control returns selected sensor's serial number.
    '''
    '''Note(s):
    '''
    '''(1) The array has to have allocated at least 128 elements.
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This driver defines the following status codes:
    '''          
    '''Status    Description
    '''-------------------------------------------------
    '''3FFF0085  Unknown status code - VI_WARN_UNKNOWN_STATUS
    '''          
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function GetSensorInfo(ByVal Channel As Integer, ByVal Sensor_Name As System.Text.StringBuilder, ByVal Sensor_Type As System.Text.StringBuilder, ByVal Sensor_Serial As System.Text.StringBuilder) As Integer
        Dim pInvokeResult As Integer = PInvoke.GetSensorInfo(Me._handle, Channel, Sensor_Name, Sensor_Type, Sensor_Serial)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function sets the sensor name.
    '''
    '''Remote-control command(s):
    '''SENS:POW:TSL:AVG:COUN
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Name">
    '''This control sets the sensor name.
    '''
    '''Valid Range:
    '''any string
    '''
    '''Default Value:
    '''""
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function setSensorName(ByVal Channel As Integer, ByVal Name As String) As Integer
        Dim pInvokeResult As Integer = PInvoke.setSensorName(Me._handle, Channel, Name)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function returns the sensor name.
    '''
    '''Remote-control command(s):
    '''SENS:POW:TSL:AVG:COUN
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Name">
    '''Returns the sensor name.
    '''
    '''</param>
    '''<param name="Max_Length">
    '''Sets the allocated size of Name buffer.
    '''
    '''Valid Values:
    '''&gt;0
    '''
    '''Default Value: 10
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function getSensorName(ByVal Channel As Integer, ByVal Name As System.Text.StringBuilder, ByVal Max_Length As UInteger) As Integer
        Dim pInvokeResult As Integer = PInvoke.getSensorName(Me._handle, Channel, Name, Max_Length)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function sets the led mode.
    '''
    '''Remote-control command(s):
    '''SYST:LED:MODE
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Mode">
    '''This control sets the led mode.
    '''
    '''Valid Range:
    '''RSNRPZ_LEDMODE_USER   (1) - User Settings
    '''RSNRPZ_LEDMODE_SENSOR (2) - Sensor Functions
    '''
    '''Default Value:
    '''RSNRPZ_LEDMODE_USER   (1)
    '''
    '''Note(s):
    '''(1) User Settings - LED is controlled by user-settings
    '''
    '''(2) Sensor Functions - LED is controlled by the sensor-functions
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function setLedMode(ByVal Channel As Integer, ByVal Mode As UInteger) As Integer
        Dim pInvokeResult As Integer = PInvoke.setLedMode(Me._handle, Channel, Mode)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function returns the led mode.
    '''
    '''Remote-control command(s):
    '''SYST:LED:MODE
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Mode">
    '''Returns the led mode.
    '''
    '''Returned Values:
    '''RSNRPZ_LEDMODE_USER   (1) - User Settings
    '''RSNRPZ_LEDMODE_SENSOR (2) - Sensor Functions
    '''
    '''Note(s):
    '''(1) User Settings - LED is controlled by user-settings
    '''
    '''(2) Sensor Functions - LED is controlled by the sensor-functions
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function getLedMode(ByVal Channel As Integer, ByRef Mode As UInteger) As Integer
        Dim pInvokeResult As Integer = PInvoke.getLedMode(Me._handle, Channel, Mode)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function sets the led color.
    '''
    '''Remote-control command(s):
    '''SYST:LED:COL
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Color">
    '''This control sets the led color.
    '''
    '''Valid Range:
    '''0 to 16777215
    '''
    '''Default Value:
    '''0
    '''
    '''Note(s):
    '''(1) Colore is 24-bit value which represents RGB values in the form of 0x00rrggbb, where rr, gg and
    '''bb are 8 bit values for red, green and blue respectively.
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function setLedColor(ByVal Channel As Integer, ByVal Color As UInteger) As Integer
        Dim pInvokeResult As Integer = PInvoke.setLedColor(Me._handle, Channel, Color)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''Returns the led color.
    '''
    '''Remote-control command(s):
    '''SYST:LED:COL
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Color">
    '''Returns the led color.
    '''
    '''Valid Range:
    '''0 to 16777215 in hex format 0x00FFFFFF
    '''
    '''
    '''Note(s):
    '''(1) Colore is 24-bit value which represents RGB values in the form of 0x00rrggbb, where rr, gg and
    '''bb are 8 bit values for red, green and blue respectively.
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function getLedColor(ByVal Channel As Integer, ByRef Color As UInteger) As Integer
        Dim pInvokeResult As Integer = PInvoke.getLedColor(Me._handle, Channel, Color)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function sets the master port.
    '''
    '''Remote-control command(s):
    '''TRIG:MAST:PORT
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Port">
    '''This control sets the master port.
    '''
    '''Valid Range:
    '''RSNRPZ_PORT_EXT1 (1) - IO Signal
    '''RSNRPZ_PORT_EXT2 (5) - TRIG 2 IO Signal
    '''
    '''Default Value:
    '''RSNRPZ_PORT_EXT1   (1)
    '''
    '''Note(s):
    '''(1) IO Signal - using the I/O signal on the Host-Interface connector
    '''
    '''(2) TRIG2 I/O signal - using TRIG2 I/O signal
    '''
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function trigger_setMasterPort(ByVal Channel As Integer, ByVal Port As UInteger) As Integer
        Dim pInvokeResult As Integer = PInvoke.trigger_setMasterPort(Me._handle, Channel, Port)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''Returns the master port.
    '''
    '''Remote-control command(s):
    '''TRIG:MAST:PORT
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Port">
    '''Returns the master port.
    '''
    '''Returned Values:
    '''RSNRPZ_PORT_EXT1 (1) - IO Signal
    '''RSNRPZ_PORT_EXT2 (5) - TRIG 2 IO Signal
    '''
    '''Note(s):
    '''(1) IO Signal - using the I/O signal on the Host-Interface connector
    '''
    '''(2) TRIG2 I/O signal - using TRIG2 I/O signal
    '''
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function trigger_getMasterPort(ByVal Channel As Integer, ByRef Port As UInteger) As Integer
        Dim pInvokeResult As Integer = PInvoke.trigger_getMasterPort(Me._handle, Channel, Port)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This function sets the sync port.
    '''
    '''Remote-control command(s):
    '''TRIG:SYNC:PORT
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Port">
    '''This control sets the sync port.
    '''
    '''Valid Range:
    '''RSNRPZ_PORT_EXT1 (1) - IO Signal
    '''RSNRPZ_PORT_EXT2 (5) - TRIG 2 IO Signal
    '''
    '''Default Value:
    '''RSNRPZ_PORT_EXT1   (1)
    '''
    '''Note(s):
    '''(1) IO Signal - using the I/O signal on the Host-Interface connector
    '''
    '''(2) TRIG2 I/O signal - using TRIG2 I/O signal
    '''
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function trigger_setSyncPort(ByVal Channel As Integer, ByVal Port As UInteger) As Integer
        Dim pInvokeResult As Integer = PInvoke.trigger_setSyncPort(Me._handle, Channel, Port)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''Returns the sync port.
    '''
    '''Remote-control command(s):
    '''TRIG:SYNC:PORT
    '''</summary>
    '''<param name="Channel">
    '''This control defines the channel number.
    '''
    '''Valid Range:
    '''1 to max available channels
    '''
    '''Default Value: 1
    '''</param>
    '''<param name="Port">
    '''Returns the sync port.
    '''
    '''Returned Values:
    '''RSNRPZ_PORT_EXT1 (1) - IO Signal
    '''RSNRPZ_PORT_EXT2 (5) - TRIG 2 IO Signal
    '''
    '''Note(s):
    '''(1) IO Signal - using the I/O signal on the Host-Interface connector
    '''
    '''(2) TRIG2 I/O signal - using TRIG2 I/O signal
    '''
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function trigger_getSyncPort(ByVal Channel As Integer, ByRef Port As UInteger) As Integer
        Dim pInvokeResult As Integer = PInvoke.trigger_getSyncPort(Me._handle, Channel, Port)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''Returns a list showing the USB resources which are in use by what application.
    '''</summary>
    '''<param name="Map">
    '''Returns a list of used sensors and their corresponding applications.
    '''</param>
    '''<param name="Max_Len">
    '''Defines size of the array pointed to by 'cp Map'.
    '''
    '''Valid Values:
    '''no range checking
    '''
    '''Default Value: 1024
    '''</param>
    '''<param name="Ret_Len">
    '''Returns how big the full list would be.
    '''
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function getUsageMap(ByVal Map As System.Text.StringBuilder, ByVal Max_Len As UInteger, ByRef Ret_Len As UInteger) As Integer
        Dim pInvokeResult As Integer = PInvoke.getUsageMap(Me._handle, Map, Max_Len, Ret_Len)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''This functions provides information about whether an NRP-Z5 is connected to the PC.
    '''</summary>
    '''<param name="Availability">
    '''This control returns whether NRP-Z5 is available on PC.
    '''
    '''Valid Values:
    '''0 - Not Available
    '''1 - Available
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This driver defines the following status codes:
    '''          
    '''Status    Description
    '''-------------------------------------------------
    '''3FFF0085  Unknown status code - VI_WARN_UNKNOWN_STATUS
    '''          
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function GetDeviceStatusZ5(ByRef Availability As Integer) As Integer
        Dim pInvokeResult As Integer = PInvoke.GetDeviceStatusZ5(Me._handle, Availability)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    '''<summary>
    '''If the above status function (= NrpGetDeviceStatusZ5() ) indicates that there is an NRP-Z5, this function supplies information about the connected devices at its ports A...D (using 'iPortIdx' = 0...3).
    '''</summary>
    '''<param name="Port">
    '''This control selects the port.
    '''
    '''Valid Values:
    '''RSNRPZ_Z5_PORT_A (0) - A
    '''RSNRPZ_Z5_PORT_B (1) - B
    '''RSNRPZ_Z5_PORT_C (2) - C
    '''RSNRPZ_Z5_PORT_D (3) - D
    '''
    '''Default Value: RSNRPZ_Z5_PORT_A (0)
    '''</param>
    '''<param name="Sensor_Name">
    '''This control returns selected sensor's name.
    '''
    '''Note(s):
    '''
    '''(1) The array has to have allocated at least 128 elements.
    '''</param>
    '''<param name="Sensor_Type">
    '''This control returns selected sensor's type.
    '''
    '''Note(s):
    '''
    '''(1) The array has to have allocated at least 128 elements.
    '''</param>
    '''<param name="Sensor_Serial">
    '''This control returns selected sensor's serial number.
    '''
    '''Note(s):
    '''
    '''(1) The array has to have allocated at least 128 elements.
    '''</param>
    '''<param name="Connected">
    '''Returns whether device is connected.
    '''
    '''Valid Values:
    '''VI_FALSE (0) - Disconnected
    '''VI_TRUE  (1) - Connected
    '''</param>
    '''<returns>
    '''Returns the status code of this operation. The status code  either indicates success or describes an error or warning condition. You examine the status code from each call to an instrument driver function to determine if an error occurred. To obtain a text description of the status code, call the rsnrpz_error_message function.
    '''          
    '''The general meaning of the status code is as follows:
    '''
    '''Value                  Meaning
    '''-------------------------------
    '''0                      Success
    '''Positive Values        Warnings
    '''Negative Values        Errors
    '''
    '''This driver defines the following status codes:
    '''          
    '''Status    Description
    '''-------------------------------------------------
    '''3FFF0085  Unknown status code - VI_WARN_UNKNOWN_STATUS
    '''          
    '''This instrument driver also returns errors and warnings defined by other sources. The following table defines the ranges of additional status codes that this driver can return. The table lists the different include files that contain the defined constants for the particular status codes:
    '''          
    '''Numeric Range (in Hex)   Status Code Types    
    '''-------------------------------------------------
    '''3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
    '''          
    '''BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    '''</returns>
    Public Function GetDeviceInfoZ5(ByVal Port As Integer, ByVal Sensor_Name As System.Text.StringBuilder, ByVal Sensor_Type As System.Text.StringBuilder, ByVal Sensor_Serial As System.Text.StringBuilder, ByRef Connected As Boolean) As Integer
        Dim ConnectedAsUShort As UShort
        Dim pInvokeResult As Integer = PInvoke.GetDeviceInfoZ5(Me._handle, Port, Sensor_Name, Sensor_Type, Sensor_Serial, ConnectedAsUShort)
        Connected = System.Convert.ToBoolean(ConnectedAsUShort)
        PInvoke.TestForError(Me._handle, pInvokeResult)
        Return pInvokeResult
    End Function
    
    Public Overloads Sub Dispose() Implements System.IDisposable.Dispose
        Me.Dispose(true)
        System.GC.SuppressFinalize(Me)
    End Sub
    
    Private Overloads Sub Dispose(ByVal disposing As Boolean)
        If (Me._disposed = false) Then
            PInvoke.close(Me._handle)
            Me._handle = New System.Runtime.InteropServices.HandleRef(Nothing, System.IntPtr.Zero)
        End If
        Me._disposed = true
    End Sub
    
    Protected Overrides Sub Finalize()
        Me.Dispose(false)
    End Sub
    
    Private Class PInvoke
        
        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_init", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function init(ByVal Resource_Name As String, ByVal ID_Query As UShort, ByVal Reset_Device As UShort, ByRef Instrument_Handle As System.IntPtr) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_init_long_distance", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function long_distance_setup(ByVal Resource_Name As String, ByVal ID_Query As UShort, ByVal Reset_Device As UShort, ByRef Instrument_Handle As System.Runtime.InteropServices.HandleRef) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_initZ5", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function initZ5(ByVal ID_Query As UShort, ByVal Port As Integer, ByVal Reset_Device As UShort, ByRef Instrument_Handle As System.IntPtr) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_chans_abort", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function chans_abort(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_chans_getCount", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function chans_getCount(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByRef Count As Integer) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_chans_initiate", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function chans_initiate(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_chans_zero", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function chans_zero(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_chans_isZeroingComplete", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function chans_isZeroingComplete(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByRef Zeroing_Completed As UShort) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_chans_isMeasurementComplete", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function chans_isMeasurementComplete(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByRef Measurement_Completed As UShort) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_chan_mode", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function chan_mode(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal Measurement_Mode As Integer) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_timing_configureExclude", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function timing_configureExclude(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal Exclude_Start As Double, ByVal Exclude_Stop As Double) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_timing_setTimingExcludeStart", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function timing_setTimingExcludeStart(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal Exclude_Start As Double) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_timing_getTimingExcludeStart", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function timing_getTimingExcludeStart(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByRef Exclude_Start As Double) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_timing_setTimingExcludeStop", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function timing_setTimingExcludeStop(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal Exclude_Stop As Double) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_timing_getTimingExcludeStop", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function timing_getTimingExcludeStop(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByRef Exclude_Stop As Double) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_bandwidth_setBw", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function bandwidth_setBw(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal Bandwidth As Integer) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_bandwidth_getBw", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function bandwidth_getBw(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByRef Bandwidth As Integer) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_bandwidth_getBwList", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function bandwidth_getBwList(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal Buffer_Size As Integer, ByVal Bandwidth_List As System.Text.StringBuilder) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_avg_configureAvgAuto", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function avg_configureAvgAuto(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal Resolution As Integer) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_avg_configureAvgNSRatio", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function avg_configureAvgNSRatio(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal Maximum_Noise_Ratio As Double, ByVal Upper_Time_Limit As Double) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_avg_configureAvgManual", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function avg_configureAvgManual(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal Count As Integer) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_avg_setAutoEnabled", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function avg_setAutoEnabled(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal Auto_Enabled As UShort) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_avg_getAutoEnabled", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function avg_getAutoEnabled(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByRef Auto_Enabled As UShort) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_avg_setAutoMaxMeasuringTime", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function avg_setAutoMaxMeasuringTime(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal Upper_Time_Limit As Double) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_avg_getAutoMaxMeasuringTime", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function avg_getAutoMaxMeasuringTime(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByRef Upper_Time_Limit As Double) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_avg_setAutoNoiseSignalRatio", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function avg_setAutoNoiseSignalRatio(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal Maximum_Noise_Ratio As Double) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_avg_getAutoNoiseSignalRatio", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function avg_getAutoNoiseSignalRatio(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByRef Maximum_Noise_Ratio As Double) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_avg_setAutoResolution", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function avg_setAutoResolution(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal Resolution As Integer) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_avg_getAutoResolution", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function avg_getAutoResolution(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByRef Resolution As Integer) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_avg_setAutoType", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function avg_setAutoType(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal Method As Integer) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_avg_getAutoType", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function avg_getAutoType(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByRef Method As Integer) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_avg_setCount", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function avg_setCount(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal Count As Integer) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_avg_getCount", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function avg_getCount(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByRef Count As Integer) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_avg_setEnabled", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function avg_setEnabled(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal Averaging As UShort) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_avg_getEnabled", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function avg_getEnabled(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByRef Averaging As UShort) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_avg_setSlot", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function avg_setSlot(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal Timeslot As Integer) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_avg_getSlot", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function avg_getSlot(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByRef Timeslot As Integer) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_avg_setTerminalControl", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function avg_setTerminalControl(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal Terminal_Control As Integer) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_avg_getTerminalControl", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function avg_getTerminalControl(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByRef Terminal_Control As Integer) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_avg_reset", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function avg_reset(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_range_setAutoEnabled", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function range_setAutoEnabled(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal Auto_Range As UShort) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_range_getAutoEnabled", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function range_getAutoEnabled(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByRef Auto_Range As UShort) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_range_setCrossoverLevel", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function range_setCrossoverLevel(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal Crossover_Level As Double) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_range_getCrossoverLevel", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function range_getCrossoverLevel(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByRef Crossover_Level As Double) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_range_setRange", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function range_setRange(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal Range As Integer) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_range_getRange", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function range_getRange(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByRef Range As Integer) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_corr_configureCorrections", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function corr_configureCorrections(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal Offset_State As UShort, ByVal Offset As Double, ByVal Reserved_1 As UShort, ByVal Reserved_2 As String, ByVal S_Parameter_Enable As UShort) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_chan_setCorrectionFrequency", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function chan_setCorrectionFrequency(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal Frequency As Double) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_chan_getCorrectionFrequency", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function chan_getCorrectionFrequency(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByRef Frequency As Double) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_chan_setCorrectionFrequencyStep", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function chan_setCorrectionFrequencyStep(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal Frequency_Step As Double) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_chan_getCorrectionFrequencyStep", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function chan_getCorrectionFrequencyStep(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByRef Frequency_Step As Double) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_chan_setCorrectionFrequencySpacing", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function chan_setCorrectionFrequencySpacing(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal Frequency_Spacing As Integer) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_chan_getCorrectionFrequencySpacing", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function chan_getCorrectionFrequencySpacing(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByRef Frequency_Spacing As Integer) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_corr_setOffset", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function corr_setOffset(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal Offset As Double) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_corr_getOffset", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function corr_getOffset(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByRef Offset As Double) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_corr_setOffsetEnabled", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function corr_setOffsetEnabled(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal Offset_State As UShort) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_corr_getOffsetEnabled", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function corr_getOffsetEnabled(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByRef Offset_State As UShort) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_corr_setSParamDeviceEnabled", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function corr_setSParamDeviceEnabled(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal S_Parameter_Enable As UShort) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_corr_getSParamDeviceEnabled", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function corr_getSParamDeviceEnabled(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByRef S_Parameter_Correction As UShort) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_corr_setSParamDevice", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function corr_setSParamDevice(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal S_Parameter As Integer) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_corr_getSParamDevice", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function corr_getSParamDevice(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByRef S_Parameter As Integer) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_corr_getSParamDevList", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function corr_getSParamDevList(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal Buffer_Size As Integer, ByVal S_Parameter_Device_List As System.Text.StringBuilder) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_chan_configureSourceGammaCorr", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function chan_configureSourceGammaCorr(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal Source_Gamma_Correction As UShort, ByVal Magnitude As Double, ByVal Phase As Double) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_chan_setSourceGammaMagnitude", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function chan_setSourceGammaMagnitude(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal Magnitude As Double) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_chan_getSourceGammaMagnitude", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function chan_getSourceGammaMagnitude(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByRef Magnitude As Double) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_chan_setSourceGammaPhase", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function chan_setSourceGammaPhase(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal Phase As Double) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_chan_getSourceGammaPhase", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function chan_getSourceGammaPhase(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByRef Phase As Double) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_chan_setSourceGammaCorrEnabled", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function chan_setSourceGammaCorrEnabled(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal Source_Gamma_Correction As UShort) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_chan_getSourceGammaCorrEnabled", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function chan_getSourceGammaCorrEnabled(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByRef Source_Gamma_Correction As UShort) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_chan_configureReflectGammaCorr", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function chan_configureReflectGammaCorr(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal Magnitude As Double, ByVal Phase As Double) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_chan_setReflectionGammaMagn", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function chan_setReflectionGammaMagn(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal Magnitude As Double) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_chan_getReflectionGammaMagn", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function chan_getReflectionGammaMagn(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByRef Magnitude As Double) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_chan_setReflectionGammaPhase", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function chan_setReflectionGammaPhase(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal Phase As Double) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_chan_getReflectionGammaPhase", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function chan_getReflectionGammaPhase(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByRef Phase As Double) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_chan_setReflectionGammaUncertainty", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function chan_setReflectionGammaUncertainty(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal Uncertainty As Double) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_chan_getReflectionGammaUncertainty", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function chan_getReflectionGammaUncertainty(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByRef Uncertainty As Double) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_corr_configureDutyCycle", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function corr_configureDutyCycle(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal Duty_Cycle_State As UShort, ByVal Duty_Cycle As Double) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_corr_setDutyCycle", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function corr_setDutyCycle(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal Duty_Cycle As Double) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_corr_getDutyCycle", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function corr_getDutyCycle(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByRef Duty_Cycle As Double) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_corr_setDutyCycleEnabled", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function corr_setDutyCycleEnabled(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal Duty_Cycle_State As UShort) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_corr_getDutyCycleEnabled", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function corr_getDutyCycleEnabled(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByRef Duty_Cycle_State As UShort) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_chan_setContAvAperture", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function chan_setContAvAperture(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal ContAv_Aperture As Double) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_chan_getContAvAperture", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function chan_getContAvAperture(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByRef ContAv_Aperture As Double) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_chan_setContAvSmoothingEnabled", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function chan_setContAvSmoothingEnabled(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal ContAv_Smoothing As UShort) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_chan_getContAvSmoothingEnabled", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function chan_getContAvSmoothingEnabled(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByRef ContAv_Smoothing As UShort) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_chan_setContAvBufferedEnabled", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function chan_setContAvBufferedEnabled(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal ContAv_Buffered_Mode As UShort) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_chan_getContAvBufferedEnabled", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function chan_getContAvBufferedEnabled(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByRef ContAv_Buffered_Mode As UShort) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_chan_setContAvBufferSize", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function chan_setContAvBufferSize(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal Buffer_Size As Integer) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_chan_getContAvBufferSize", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function chan_getContAvBufferSize(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByRef Buffer_Size As Integer) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_chan_getContAvBufferCount", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function chan_getContAvBufferCount(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByRef Buffer_Count As Integer) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_chan_getContAvBufferInfo", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function chan_getContAvBufferInfo(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal Info_Type As String, ByVal Array_Size As Integer, ByVal Info As System.Text.StringBuilder) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_chan_setBurstDropoutTolerance", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function chan_setBurstDropoutTolerance(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal Drop_out_Tolerance As Double) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_chan_getBurstDropoutTolerance", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function chan_getBurstDropoutTolerance(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByRef Drop_out_Tolerance As Double) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_chan_setBurstChopperEnabled", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function chan_setBurstChopperEnabled(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal BurstAv_Chopper As UShort) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_chan_getBurstChopperEnabled", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function chan_getBurstChopperEnabled(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByRef BurstAv_Chopper As UShort) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_stat_confTimegate", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function stat_confTimegate(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal Offset As Double, ByVal Time As Double, ByVal Midamble_Offset As Double, ByVal Midamble_Length As Double) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_stat_confScale", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function stat_confScale(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal Reference_Level As Double, ByVal Range As Double, ByVal Points As Integer) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_stat_setOffsetTime", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function stat_setOffsetTime(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal Offset As Double) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_stat_getOffsetTime", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function stat_getOffsetTime(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByRef Offset As Double) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_stat_setTime", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function stat_setTime(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal Time As Double) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_stat_getTime", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function stat_getTime(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByRef Time As Double) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_stat_setMidOffset", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function stat_setMidOffset(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal Offset As Double) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_stat_getMidOffset", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function stat_getMidOffset(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByRef Offset As Double) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_stat_setMidLength", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function stat_setMidLength(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal Length As Double) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_stat_getMidLength", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function stat_getMidLength(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByRef Length As Double) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_stat_setScaleRefLevel", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function stat_setScaleRefLevel(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal Reference_Level As Double) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_stat_getScaleRefLevel", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function stat_getScaleRefLevel(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByRef Reference_Level As Double) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_stat_setScaleRange", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function stat_setScaleRange(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal Range As Double) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_stat_getScaleRange", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function stat_getScaleRange(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByRef Range As Double) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_stat_setScalePoints", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function stat_setScalePoints(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal Points As Integer) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_stat_getScalePoints", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function stat_getScalePoints(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByRef Points As Integer) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_stat_getScaleWidth", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function stat_getScaleWidth(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByRef Width As Double) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_tslot_configureTimeSlot", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function tslot_configureTimeSlot(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal Time_Slot_Count As Integer, ByVal Width As Double) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_tslot_setTimeSlotCount", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function tslot_setTimeSlotCount(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal Time_Slot_Count As Integer) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_tslot_getTimeSlotCount", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function tslot_getTimeSlotCount(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByRef Time_Slot_Count As Integer) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_tslot_setTimeSlotWidth", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function tslot_setTimeSlotWidth(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal Width As Double) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_tslot_getTimeSlotWidth", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function tslot_getTimeSlotWidth(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByRef Width As Double) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_tslot_setTimeSlotMidOffset", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function tslot_setTimeSlotMidOffset(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal Offset As Double) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_tslot_getTimeSlotMidOffset", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function tslot_getTimeSlotMidOffset(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByRef Offset As Double) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_tslot_setTimeSlotMidLength", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function tslot_setTimeSlotMidLength(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal Length As Double) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_tslot_getTimeSlotMidLength", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function tslot_getTimeSlotMidLength(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByRef Length As Double) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_tslot_setTimeSlotChopperEnabled", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function tslot_setTimeSlotChopperEnabled(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal Time_Slot_Chopper As UShort) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_tslot_getTimeSlotChopperEnabled", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function tslot_getTimeSlotChopperEnabled(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByRef Time_Slot_Chopper As UShort) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_scope_configureScope", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function scope_configureScope(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal Scope_Points As Integer, ByVal Scope_Time As Double, ByVal Offset_Time As Double, ByVal Realtime As UShort) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_scope_fastZero", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function scope_fastZero(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_scope_setAverageEnabled", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function scope_setAverageEnabled(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal Scope_Averaging As UShort) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_scope_getAverageEnabled", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function scope_getAverageEnabled(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByRef Scope_Averaging As UShort) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_scope_setAverageCount", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function scope_setAverageCount(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal Count As Integer) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_scope_getAverageCount", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function scope_getAverageCount(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByRef Count As Integer) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_scope_setAverageTerminalControl", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function scope_setAverageTerminalControl(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal Terminal_Control As Integer) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_scope_getAverageTerminalControl", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function scope_getAverageTerminalControl(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByRef Terminal_Control As Integer) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_scope_setOffsetTime", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function scope_setOffsetTime(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal Offset_Time As Double) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_scope_getOffsetTime", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function scope_getOffsetTime(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByRef Offset_Time As Double) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_scope_setPoints", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function scope_setPoints(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal Scope_Points As Integer) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_scope_getPoints", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function scope_getPoints(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByRef Scope_Points As Integer) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_scope_setRealtimeEnabled", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function scope_setRealtimeEnabled(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal Realtime As UShort) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_scope_getRealtimeEnabled", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function scope_getRealtimeEnabled(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByRef Realtime As UShort) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_scope_setTime", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function scope_setTime(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal Scope_Time As Double) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_scope_getTime", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function scope_getTime(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByRef Scope_Time As Double) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_scope_setAutoEnabled", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function scope_setAutoEnabled(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal Auto_Enabled As UShort) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_scope_getAutoEnabled", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function scope_getAutoEnabled(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByRef Auto_Enabled As UShort) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_scope_setAutoMaxMeasuringTime", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function scope_setAutoMaxMeasuringTime(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal Upper_Time_Limit As Double) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_scope_getAutoMaxMeasuringTime", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function scope_getAutoMaxMeasuringTime(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByRef Upper_Time_Limit As Double) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_scope_setAutoNoiseSignalRatio", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function scope_setAutoNoiseSignalRatio(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal Maximum_Noise_Ratio As Double) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_scope_getAutoNoiseSignalRatio", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function scope_getAutoNoiseSignalRatio(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByRef Maximum_Noise_Ratio As Double) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_scope_setAutoResolution", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function scope_setAutoResolution(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal Resolution As Integer) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_scope_getAutoResolution", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function scope_getAutoResolution(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByRef Resolution As Integer) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_scope_setAutoType", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function scope_setAutoType(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal Method As Integer) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_scope_getAutoType", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function scope_getAutoType(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByRef Method As Integer) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_scope_setEquivalentSampling", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function scope_setEquivalentSampling(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal Scope_Equivalent_Sampling As UShort) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_scope_getEquivalentSampling", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function scope_getEquivalentSampling(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByRef Scope_Equivalent_Sampling As UShort) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_scope_meas_setMeasEnabled", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function scope_meas_setMeasEnabled(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal Trace_Measurements As UShort) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_scope_meas_getMeasEnabled", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function scope_meas_getMeasEnabled(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByRef Trace_Measurements As UShort) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_scope_meas_setMeasAlgorithm", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function scope_meas_setMeasAlgorithm(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal Algorithm As Integer) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_scope_meas_getMeasAlgorithm", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function scope_meas_getMeasAlgorithm(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByRef Algorithm As Integer) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_scope_meas_setLevelThresholds", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function scope_meas_setLevelThresholds(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal Duration_Ref As Double, ByVal Transition_Low_Ref As Double, ByVal Transition_High_Ref As Double) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_scope_meas_getLevelThresholds", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function scope_meas_getLevelThresholds(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByRef Duration_Ref As Double, ByRef Transition_Low_Ref As Double, ByRef Transition_High_Ref As Double) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_scope_meas_setTime", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function scope_meas_setTime(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal Meas_Time As Double) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_scope_meas_getTime", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function scope_meas_getTime(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByRef Meas_Time As Double) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_scope_meas_setOffsetTime", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function scope_meas_setOffsetTime(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal Offset_Time As Double) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_scope_meas_getOffsetTime", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function scope_meas_getOffsetTime(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByRef Offset_Time As Double) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_scope_meas_getPulseTimes", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function scope_meas_getPulseTimes(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByRef Duty_Cycle As Double, ByRef Pulse_Duration As Double, ByRef Pulse_Period As Double) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_scope_meas_getPulseTransition", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function scope_meas_getPulseTransition(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal Slope As Integer, ByRef Duration As Double, ByRef Occurence As Double, ByRef Overshoot As Double) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_scope_meas_getPulsePower", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function scope_meas_getPulsePower(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByRef Average As Double, ByRef Min_Peak As Double, ByRef Max_Peak As Double) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_scope_meas_getPulseLevels", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function scope_meas_getPulseLevels(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByRef Top_Level As Double, ByRef Base_Level As Double) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_scope_meas_getPulseReferenceLevels", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function scope_meas_getPulseReferenceLevels(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByRef Low_Ref_Level As Double, ByRef High_Ref_Level As Double, ByRef Duration_Ref_Level As Double) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_scope_meas_setEquivalentSampling", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function scope_meas_setEquivalentSampling(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal Scope_Meas_Equiv_Sampling As UShort) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_scope_meas_getEquivalentSampling", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function scope_meas_getEquivalentSampling(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByRef Scope_Meas_Equiv_Sampling As UShort) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_scope_meas_getSamplePeriod", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function scope_meas_getSamplePeriod(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByRef Sample_Period As Double) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_trigger_configureInternal", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function trigger_configureInternal(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal Trigger_Level As Double, ByVal Trigger_Slope As Integer) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_trigger_configureExternal", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function trigger_configureExternal(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal Trigger_Delay As Double) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_trigger_immediate", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function trigger_immediate(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_trigger_setAutoDelayEnabled", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function trigger_setAutoDelayEnabled(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal Auto_Delay As UShort) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_trigger_getAutoDelayEnabled", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function trigger_getAutoDelayEnabled(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByRef Auto_Delay As UShort) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_trigger_setAutoTriggerEnabled", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function trigger_setAutoTriggerEnabled(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal Auto_Trigger As UShort) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_trigger_getAutoTriggerEnabled", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function trigger_getAutoTriggerEnabled(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByRef Auto_Trigger As UShort) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_trigger_setCount", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function trigger_setCount(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal Trigger_Count As Integer) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_trigger_getCount", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function trigger_getCount(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByRef Trigger_Count As Integer) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_trigger_setDelay", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function trigger_setDelay(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal Trigger_Delay As Double) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_trigger_getDelay", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function trigger_getDelay(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByRef Trigger_Delay As Double) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_trigger_setHoldoff", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function trigger_setHoldoff(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal Trigger_Holdoff As Double) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_trigger_getHoldoff", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function trigger_getHoldoff(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByRef Trigger_Holdoff As Double) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_trigger_setHysteresis", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function trigger_setHysteresis(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal Trigger_Hysteresis As Double) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_trigger_getHysteresis", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function trigger_getHysteresis(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByRef Trigger_Hysteresis As Double) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_trigger_setLevel", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function trigger_setLevel(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal Trigger_Level As Double) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_trigger_getLevel", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function trigger_getLevel(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByRef Trigger_Level As Double) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_trigger_setSlope", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function trigger_setSlope(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal Trigger_Slope As Integer) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_trigger_getSlope", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function trigger_getSlope(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByRef Trigger_Slope As Integer) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_trigger_setSource", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function trigger_setSource(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal Trigger_Source As Integer) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_trigger_getSource", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function trigger_getSource(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByRef Trigger_Source As Integer) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_trigger_setDropoutTime", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function trigger_setDropoutTime(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal Dropout_Time As Double) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_trigger_getDropoutTime", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function trigger_getDropoutTime(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByRef Dropout_Time As Double) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_trigger_setMasterState", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function trigger_setMasterState(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal State As UShort) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_trigger_getMasterState", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function trigger_getMasterState(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByRef State As UShort) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_trigger_setSyncState", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function trigger_setSyncState(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal State As UShort) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_trigger_getSyncState", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function trigger_getSyncState(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByRef State As UShort) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_chan_info", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function chan_info(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal Info_Type As String, ByVal Array_Size As Integer, ByVal Info As System.Text.StringBuilder) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_chan_infoHeader", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function chan_infoHeader(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal Parameter_Number As Integer, ByVal Array_Size As Integer, ByVal Header As System.Text.StringBuilder) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_chan_infosCount", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function chan_infosCount(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByRef Count As Integer) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_fw_version_check", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function fw_version_check(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Buffer_Size As Integer, ByVal Firmware_Current As System.Text.StringBuilder, ByVal Firmware_Required_Minimum As System.Text.StringBuilder, ByRef Firmware_Okay As UShort) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_system_setStatusUpdateTime", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function system_setStatusUpdateTime(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal Status_Update_Time As Double) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_system_getStatusUpdateTime", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function system_getStatusUpdateTime(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByRef Status_Update_Time As Double) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_system_setResultUpdateTime", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function system_setResultUpdateTime(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal Result_Update_Time As Double) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_system_getResultUpdateTime", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function system_getResultUpdateTime(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByRef Result_Update_Time As Double) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_calib_test", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function calib_test(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByRef Calib_Test_2 As Double) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_calib_getTestDeviation", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function calib_getTestDeviation(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByRef Test_Deviation As Double) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_calib_getTestReference", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function calib_getTestReference(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByRef Test_Reference As Double) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_chan_abort", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function chan_abort(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_chan_initiate", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function chan_initiate(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_chan_setInitContinuousEnabled", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function chan_setInitContinuousEnabled(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal Continuous_Initiate As UShort) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_chan_getInitContinuousEnabled", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function chan_getInitContinuousEnabled(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByRef Continuous_Initiate As UShort) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_chan_reset", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function chan_reset(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_chan_setSamplingFrequency", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function chan_setSamplingFrequency(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal Sampling_Frequency As Integer) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_chan_getSamplingFrequency", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function chan_getSamplingFrequency(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByRef Sampling_Frequency As Integer) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_chan_zero", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function chan_zero(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_chan_zeroAdvanced", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function chan_zeroAdvanced(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal Zeroing As Integer) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_chan_isZeroComplete", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function chan_isZeroComplete(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByRef Zeroing_Complete As UShort) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_chan_isMeasurementComplete", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function chan_isMeasurementComplete(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByRef Measurement_Complete As UShort) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_chan_selfTest", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function chan_selfTest(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal Result As System.Text.StringBuilder) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_chan_setAuxiliary", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function chan_setAuxiliary(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal Auxiliary_Value As Integer) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_chan_getAuxiliary", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function chan_getAuxiliary(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByRef Auxiliary_Value As Integer) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_meass_readMeasurement", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function meass_readMeasurement(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal Timeout__ms_ As Integer, ByRef Measurement As Double) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_meass_fetchMeasurement", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function meass_fetchMeasurement(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByRef Measurement As Double) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_meass_readBufferMeasurement", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function meass_readBufferMeasurement(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal Maximum_Time__ms_ As Integer, ByVal Buffer_Size As Integer, <[In](), Out()> Measurement_Array As Double(), ByRef Read_Count As Integer) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_meass_fetchBufferMeasurement", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function meass_fetchBufferMeasurement(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal Array_Size As Integer, <[In](), Out()> Measurement_Array As Double(), ByRef Read_Count As Integer) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_meass_sendSoftwareTrigger", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function meass_sendSoftwareTrigger(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_meass_readMeasurementAux", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function meass_readMeasurementAux(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal Timeout__ms_ As Integer, ByRef Measurement As Double, ByRef Aux_1 As Double, ByRef Aux_2 As Double) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_meass_fetchMeasurementAux", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function meass_fetchMeasurementAux(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal Timeout__ms_ As Integer, ByRef Measurement As Double, ByRef Aux_1 As Double, ByRef Aux_2 As Double) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_meass_readBufferMeasurementAux", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function meass_readBufferMeasurementAux(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal Maximum_Time__ms_ As Integer, ByVal Buffer_Size As Integer, <[In](), Out()> Measurement_Array As Double(), <[In](), Out()> Aux_1_Array As Double(), <[In](), Out()> Aux_2_Array As Double(), ByRef Read_Count As Integer) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_meass_fetchBufferMeasurementAux", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function meass_fetchBufferMeasurementAux(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal Maximum_Time__ms_ As Integer, ByVal Buffer_Size As Integer, <[In](), Out()> Measurement_Array As Double(), <[In](), Out()> Aux_1_Array As Double(), <[In](), Out()> Aux_2_Array As Double(), ByRef Read_Count As Integer) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_status_preset", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function status_preset(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_status_checkCondition", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function status_checkCondition(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Status_Class As Integer, ByVal Mask As UInteger, ByRef State As UShort) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_status_catchEvent", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function status_catchEvent(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Status_Class As Integer, ByVal Mask As UInteger, ByVal Direction As Integer) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_status_checkEvent", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function status_checkEvent(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Status_Class As Integer, ByVal Mask As UInteger, ByVal Reset_Mask As UInteger, ByRef Events As UShort) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_status_enableEventNotification", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function status_enableEventNotification(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Status_Class As Integer, ByVal Mask As UInteger) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_status_disableEventNotification", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function status_disableEventNotification(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Status_Class As Integer, ByVal Mask As UInteger) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_status_driverOpenState", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function status_driverOpenState(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByRef Driver_State As UShort) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_status_registerWindowMessage", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function status_registerWindowMessage(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Window_Handle As UInteger, ByVal Message_ID As UInteger) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_service_getDetectorTemperature", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function service_getDetectorTemperature(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByRef Temperature As Double) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_service_startSimulation", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function service_startSimulation(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal Block_Count As Integer) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_service_setSimulationValues", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function service_setSimulationValues(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal Value_Count As Integer(), ByVal Values As Double()) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_service_stopSimulation", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function service_stopSimulation(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_errorCheckState", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function errorCheckState(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal State_Checking As UShort) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_reset", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function reset(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_self_test", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function self_test(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByRef Self_Test_Result As Short, ByVal Self_Test_Message As System.Text.StringBuilder) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_error_query", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function error_query(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByRef Error_Code As Integer, ByVal Error_Message As System.Text.StringBuilder) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_revision_query", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function revision_query(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Instrument_Driver_Revision As System.Text.StringBuilder, ByVal Firmware_Revision As System.Text.StringBuilder) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_GetSensorCount", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function GetSensorCount(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByRef Sensor_Count As Integer) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_GetSensorInfo", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function GetSensorInfo(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal Sensor_Name As System.Text.StringBuilder, ByVal Sensor_Type As System.Text.StringBuilder, ByVal Sensor_Serial As System.Text.StringBuilder) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_setSensorName", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function setSensorName(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal Name As String) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_getSensorName", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function getSensorName(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal Name As System.Text.StringBuilder, ByVal Max_Length As UInteger) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_setLedMode", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function setLedMode(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal Mode As UInteger) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_getLedMode", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function getLedMode(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByRef Mode As UInteger) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_setLedColor", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function setLedColor(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal Color As UInteger) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_getLedColor", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function getLedColor(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByRef Color As UInteger) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_trigger_setMasterPort", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function trigger_setMasterPort(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal Port As UInteger) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_trigger_getMasterPort", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function trigger_getMasterPort(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByRef Port As UInteger) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_trigger_setSyncPort", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function trigger_setSyncPort(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByVal Port As UInteger) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_trigger_getSyncPort", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function trigger_getSyncPort(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Channel As Integer, ByRef Port As UInteger) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_getUsageMap", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function getUsageMap(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Map As System.Text.StringBuilder, ByVal Max_Len As UInteger, ByRef Ret_Len As UInteger) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_GetDeviceStatusZ5", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function GetDeviceStatusZ5(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByRef Availability As Integer) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_GetDeviceInfoZ5", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function GetDeviceInfoZ5(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Port As Integer, ByVal Sensor_Name As System.Text.StringBuilder, ByVal Sensor_Type As System.Text.StringBuilder, ByVal Sensor_Serial As System.Text.StringBuilder, ByRef Connected As UShort) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_close", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function close(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef) As Integer
        End Function

        <DllImport("rsnrpz_64.dll", EntryPoint:="rsnrpz_error_message", CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function error_message(ByVal Instrument_Handle As System.Runtime.InteropServices.HandleRef, ByVal Status_Code As Integer, ByVal Message As System.Text.StringBuilder) As Integer
        End Function

        
        Public Shared Function TestForError(ByVal handle As System.Runtime.InteropServices.HandleRef, ByVal status As Integer) As Integer
            If (status < 0) Then
                PInvoke.ThrowError(handle, status)
            End If
            Return status
        End Function
        
        Public Shared Function ThrowError(ByVal handle As System.Runtime.InteropServices.HandleRef, ByVal code As Integer) As Integer
            Dim msg As System.Text.StringBuilder = New System.Text.StringBuilder(256)
            PInvoke.error_message(handle, code, msg)
            Throw New System.Runtime.InteropServices.ExternalException(msg.ToString, code)
        End Function
    End Class
End Class

Public Class rsnrpzConstants
    
    Public Const SensorModeContav As Integer = 0
    
    Public Const SensorModeBufContav As Integer = 1
    
    Public Const SensorModeTimeslot As Integer = 2
    
    Public Const SensorModeBurst As Integer = 3
    
    Public Const SensorModeScope As Integer = 4
    
    Public Const SensorModeCcdf As Integer = 6
    
    Public Const SensorModePdf As Integer = 7
    
    Public Const AutoCountTypeResolution As Integer = 0
    
    Public Const AutoCountTypeNsr As Integer = 1
    
    Public Const TerminalControlMoving As Integer = 0
    
    Public Const TerminalControlRepeat As Integer = 1
    
    Public Const SpacingLinear As Integer = 0
    
    Public Const SpacingLog As Integer = 1
    
    Public Const ScopeMeasAlgHist As Integer = 0
    
    Public Const ScopeMeasAlgInt As Integer = 1
    
    Public Const SlopePositive As Integer = 0
    
    Public Const SlopeNegative As Integer = 1
    
    Public Const TriggerSourceBus As Integer = 0
    
    Public Const TriggerSourceExternal As Integer = 1
    
    Public Const TriggerSourceHold As Integer = 2
    
    Public Const TriggerSourceImmediate As Integer = 3
    
    Public Const TriggerSourceInternal As Integer = 4
    
    Public Const SamplingFrequency1 As Integer = 1
    
    Public Const SamplingFrequency2 As Integer = 2
    
    Public Const ZeroLfr As Integer = 4
    
    Public Const ZeroUfr As Integer = 5
    
    Public Const AuxNone As Integer = 0
    
    Public Const AuxMinmax As Integer = 1
    
    Public Const AuxRndmax As Integer = 2
    
    Public Const StatclassDConn As Integer = 1
    
    Public Const StatclassDErr As Integer = 2
    
    Public Const StatclassOCal As Integer = 3
    
    Public Const StatclassOMeas As Integer = 4
    
    Public Const StatclassOTrigger As Integer = 5
    
    Public Const StatclassOSense As Integer = 6
    
    Public Const StatclassOLower As Integer = 7
    
    Public Const StatclassOUpper As Integer = 8
    
    Public Const StatclassQPower As Integer = 9
    
    Public Const StatclassQWindow As Integer = 10
    
    Public Const StatclassQCal As Integer = 11
    
    Public Const StatclassQZer As Integer = 12
    
    Public Const DirectionPtr As Integer = 1
    
    Public Const DirectionNtr As Integer = 2
    
    Public Const DirectionBoth As Integer = 3
    
    Public Const DirectionNone As Integer = 0
    
    Public Const LedmodeUser As UInteger = 1
    
    Public Const LedmodeSensor As UInteger = 2
    
    Public Const PortExt1 As UInteger = 1
    
    Public Const PortExt2 As UInteger = 5
    
    Public Const Z5PortA As Integer = 0
    
    Public Const Z5PortB As Integer = 1
    
    Public Const Z5PortC As Integer = 2
    
    Public Const Z5PortD As Integer = 3
End Class
