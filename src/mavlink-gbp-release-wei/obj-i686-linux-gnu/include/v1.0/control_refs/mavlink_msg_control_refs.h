// MESSAGE CONTROL_REFS PACKING

#define MAVLINK_MSG_ID_CONTROL_REFS 150

typedef struct __mavlink_control_refs_t
{
 float theta; ///< Theta.
 float phi; ///< Phi
 float psi; ///< Psi
 float thrust; ///< Thrust
} mavlink_control_refs_t;

#define MAVLINK_MSG_ID_CONTROL_REFS_LEN 16
#define MAVLINK_MSG_ID_150_LEN 16

#define MAVLINK_MSG_ID_CONTROL_REFS_CRC 87
#define MAVLINK_MSG_ID_150_CRC 87



#define MAVLINK_MESSAGE_INFO_CONTROL_REFS { \
	"CONTROL_REFS", \
	4, \
	{  { "theta", NULL, MAVLINK_TYPE_FLOAT, 0, 0, offsetof(mavlink_control_refs_t, theta) }, \
         { "phi", NULL, MAVLINK_TYPE_FLOAT, 0, 4, offsetof(mavlink_control_refs_t, phi) }, \
         { "psi", NULL, MAVLINK_TYPE_FLOAT, 0, 8, offsetof(mavlink_control_refs_t, psi) }, \
         { "thrust", NULL, MAVLINK_TYPE_FLOAT, 0, 12, offsetof(mavlink_control_refs_t, thrust) }, \
         } \
}


/**
 * @brief Pack a control_refs message
 * @param system_id ID of this system
 * @param component_id ID of this component (e.g. 200 for IMU)
 * @param msg The MAVLink message to compress the data into
 *
 * @param theta Theta.
 * @param phi Phi
 * @param psi Psi
 * @param thrust Thrust
 * @return length of the message in bytes (excluding serial stream start sign)
 */
static inline uint16_t mavlink_msg_control_refs_pack(uint8_t system_id, uint8_t component_id, mavlink_message_t* msg,
						       float theta, float phi, float psi, float thrust)
{
#if MAVLINK_NEED_BYTE_SWAP || !MAVLINK_ALIGNED_FIELDS
	char buf[MAVLINK_MSG_ID_CONTROL_REFS_LEN];
	_mav_put_float(buf, 0, theta);
	_mav_put_float(buf, 4, phi);
	_mav_put_float(buf, 8, psi);
	_mav_put_float(buf, 12, thrust);

        memcpy(_MAV_PAYLOAD_NON_CONST(msg), buf, MAVLINK_MSG_ID_CONTROL_REFS_LEN);
#else
	mavlink_control_refs_t packet;
	packet.theta = theta;
	packet.phi = phi;
	packet.psi = psi;
	packet.thrust = thrust;

        memcpy(_MAV_PAYLOAD_NON_CONST(msg), &packet, MAVLINK_MSG_ID_CONTROL_REFS_LEN);
#endif

	msg->msgid = MAVLINK_MSG_ID_CONTROL_REFS;
#if MAVLINK_CRC_EXTRA
    return mavlink_finalize_message(msg, system_id, component_id, MAVLINK_MSG_ID_CONTROL_REFS_LEN, MAVLINK_MSG_ID_CONTROL_REFS_CRC);
#else
    return mavlink_finalize_message(msg, system_id, component_id, MAVLINK_MSG_ID_CONTROL_REFS_LEN);
#endif
}

/**
 * @brief Pack a control_refs message on a channel
 * @param system_id ID of this system
 * @param component_id ID of this component (e.g. 200 for IMU)
 * @param chan The MAVLink channel this message will be sent over
 * @param msg The MAVLink message to compress the data into
 * @param theta Theta.
 * @param phi Phi
 * @param psi Psi
 * @param thrust Thrust
 * @return length of the message in bytes (excluding serial stream start sign)
 */
static inline uint16_t mavlink_msg_control_refs_pack_chan(uint8_t system_id, uint8_t component_id, uint8_t chan,
							   mavlink_message_t* msg,
						           float theta,float phi,float psi,float thrust)
{
#if MAVLINK_NEED_BYTE_SWAP || !MAVLINK_ALIGNED_FIELDS
	char buf[MAVLINK_MSG_ID_CONTROL_REFS_LEN];
	_mav_put_float(buf, 0, theta);
	_mav_put_float(buf, 4, phi);
	_mav_put_float(buf, 8, psi);
	_mav_put_float(buf, 12, thrust);

        memcpy(_MAV_PAYLOAD_NON_CONST(msg), buf, MAVLINK_MSG_ID_CONTROL_REFS_LEN);
#else
	mavlink_control_refs_t packet;
	packet.theta = theta;
	packet.phi = phi;
	packet.psi = psi;
	packet.thrust = thrust;

        memcpy(_MAV_PAYLOAD_NON_CONST(msg), &packet, MAVLINK_MSG_ID_CONTROL_REFS_LEN);
#endif

	msg->msgid = MAVLINK_MSG_ID_CONTROL_REFS;
#if MAVLINK_CRC_EXTRA
    return mavlink_finalize_message_chan(msg, system_id, component_id, chan, MAVLINK_MSG_ID_CONTROL_REFS_LEN, MAVLINK_MSG_ID_CONTROL_REFS_CRC);
#else
    return mavlink_finalize_message_chan(msg, system_id, component_id, chan, MAVLINK_MSG_ID_CONTROL_REFS_LEN);
#endif
}

/**
 * @brief Encode a control_refs struct
 *
 * @param system_id ID of this system
 * @param component_id ID of this component (e.g. 200 for IMU)
 * @param msg The MAVLink message to compress the data into
 * @param control_refs C-struct to read the message contents from
 */
static inline uint16_t mavlink_msg_control_refs_encode(uint8_t system_id, uint8_t component_id, mavlink_message_t* msg, const mavlink_control_refs_t* control_refs)
{
	return mavlink_msg_control_refs_pack(system_id, component_id, msg, control_refs->theta, control_refs->phi, control_refs->psi, control_refs->thrust);
}

/**
 * @brief Encode a control_refs struct on a channel
 *
 * @param system_id ID of this system
 * @param component_id ID of this component (e.g. 200 for IMU)
 * @param chan The MAVLink channel this message will be sent over
 * @param msg The MAVLink message to compress the data into
 * @param control_refs C-struct to read the message contents from
 */
static inline uint16_t mavlink_msg_control_refs_encode_chan(uint8_t system_id, uint8_t component_id, uint8_t chan, mavlink_message_t* msg, const mavlink_control_refs_t* control_refs)
{
	return mavlink_msg_control_refs_pack_chan(system_id, component_id, chan, msg, control_refs->theta, control_refs->phi, control_refs->psi, control_refs->thrust);
}

/**
 * @brief Send a control_refs message
 * @param chan MAVLink channel to send the message
 *
 * @param theta Theta.
 * @param phi Phi
 * @param psi Psi
 * @param thrust Thrust
 */
#ifdef MAVLINK_USE_CONVENIENCE_FUNCTIONS

static inline void mavlink_msg_control_refs_send(mavlink_channel_t chan, float theta, float phi, float psi, float thrust)
{
#if MAVLINK_NEED_BYTE_SWAP || !MAVLINK_ALIGNED_FIELDS
	char buf[MAVLINK_MSG_ID_CONTROL_REFS_LEN];
	_mav_put_float(buf, 0, theta);
	_mav_put_float(buf, 4, phi);
	_mav_put_float(buf, 8, psi);
	_mav_put_float(buf, 12, thrust);

#if MAVLINK_CRC_EXTRA
    _mav_finalize_message_chan_send(chan, MAVLINK_MSG_ID_CONTROL_REFS, buf, MAVLINK_MSG_ID_CONTROL_REFS_LEN, MAVLINK_MSG_ID_CONTROL_REFS_CRC);
#else
    _mav_finalize_message_chan_send(chan, MAVLINK_MSG_ID_CONTROL_REFS, buf, MAVLINK_MSG_ID_CONTROL_REFS_LEN);
#endif
#else
	mavlink_control_refs_t packet;
	packet.theta = theta;
	packet.phi = phi;
	packet.psi = psi;
	packet.thrust = thrust;

#if MAVLINK_CRC_EXTRA
    _mav_finalize_message_chan_send(chan, MAVLINK_MSG_ID_CONTROL_REFS, (const char *)&packet, MAVLINK_MSG_ID_CONTROL_REFS_LEN, MAVLINK_MSG_ID_CONTROL_REFS_CRC);
#else
    _mav_finalize_message_chan_send(chan, MAVLINK_MSG_ID_CONTROL_REFS, (const char *)&packet, MAVLINK_MSG_ID_CONTROL_REFS_LEN);
#endif
#endif
}

#if MAVLINK_MSG_ID_CONTROL_REFS_LEN <= MAVLINK_MAX_PAYLOAD_LEN
/*
  This varient of _send() can be used to save stack space by re-using
  memory from the receive buffer.  The caller provides a
  mavlink_message_t which is the size of a full mavlink message. This
  is usually the receive buffer for the channel, and allows a reply to an
  incoming message with minimum stack space usage.
 */
static inline void mavlink_msg_control_refs_send_buf(mavlink_message_t *msgbuf, mavlink_channel_t chan,  float theta, float phi, float psi, float thrust)
{
#if MAVLINK_NEED_BYTE_SWAP || !MAVLINK_ALIGNED_FIELDS
	char *buf = (char *)msgbuf;
	_mav_put_float(buf, 0, theta);
	_mav_put_float(buf, 4, phi);
	_mav_put_float(buf, 8, psi);
	_mav_put_float(buf, 12, thrust);

#if MAVLINK_CRC_EXTRA
    _mav_finalize_message_chan_send(chan, MAVLINK_MSG_ID_CONTROL_REFS, buf, MAVLINK_MSG_ID_CONTROL_REFS_LEN, MAVLINK_MSG_ID_CONTROL_REFS_CRC);
#else
    _mav_finalize_message_chan_send(chan, MAVLINK_MSG_ID_CONTROL_REFS, buf, MAVLINK_MSG_ID_CONTROL_REFS_LEN);
#endif
#else
	mavlink_control_refs_t *packet = (mavlink_control_refs_t *)msgbuf;
	packet->theta = theta;
	packet->phi = phi;
	packet->psi = psi;
	packet->thrust = thrust;

#if MAVLINK_CRC_EXTRA
    _mav_finalize_message_chan_send(chan, MAVLINK_MSG_ID_CONTROL_REFS, (const char *)packet, MAVLINK_MSG_ID_CONTROL_REFS_LEN, MAVLINK_MSG_ID_CONTROL_REFS_CRC);
#else
    _mav_finalize_message_chan_send(chan, MAVLINK_MSG_ID_CONTROL_REFS, (const char *)packet, MAVLINK_MSG_ID_CONTROL_REFS_LEN);
#endif
#endif
}
#endif

#endif

// MESSAGE CONTROL_REFS UNPACKING


/**
 * @brief Get field theta from control_refs message
 *
 * @return Theta.
 */
static inline float mavlink_msg_control_refs_get_theta(const mavlink_message_t* msg)
{
	return _MAV_RETURN_float(msg,  0);
}

/**
 * @brief Get field phi from control_refs message
 *
 * @return Phi
 */
static inline float mavlink_msg_control_refs_get_phi(const mavlink_message_t* msg)
{
	return _MAV_RETURN_float(msg,  4);
}

/**
 * @brief Get field psi from control_refs message
 *
 * @return Psi
 */
static inline float mavlink_msg_control_refs_get_psi(const mavlink_message_t* msg)
{
	return _MAV_RETURN_float(msg,  8);
}

/**
 * @brief Get field thrust from control_refs message
 *
 * @return Thrust
 */
static inline float mavlink_msg_control_refs_get_thrust(const mavlink_message_t* msg)
{
	return _MAV_RETURN_float(msg,  12);
}

/**
 * @brief Decode a control_refs message into a struct
 *
 * @param msg The message to decode
 * @param control_refs C-struct to decode the message contents into
 */
static inline void mavlink_msg_control_refs_decode(const mavlink_message_t* msg, mavlink_control_refs_t* control_refs)
{
#if MAVLINK_NEED_BYTE_SWAP
	control_refs->theta = mavlink_msg_control_refs_get_theta(msg);
	control_refs->phi = mavlink_msg_control_refs_get_phi(msg);
	control_refs->psi = mavlink_msg_control_refs_get_psi(msg);
	control_refs->thrust = mavlink_msg_control_refs_get_thrust(msg);
#else
	memcpy(control_refs, _MAV_PAYLOAD(msg), MAVLINK_MSG_ID_CONTROL_REFS_LEN);
#endif
}
