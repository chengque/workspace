/** @file
 *	@brief MAVLink comm protocol testsuite generated from control_refs.xml
 *	@see http://qgroundcontrol.org/mavlink/
 */
#ifndef CONTROL_REFS_TESTSUITE_H
#define CONTROL_REFS_TESTSUITE_H

#ifdef __cplusplus
extern "C" {
#endif

#ifndef MAVLINK_TEST_ALL
#define MAVLINK_TEST_ALL
static void mavlink_test_common(uint8_t, uint8_t, mavlink_message_t *last_msg);
static void mavlink_test_control_refs(uint8_t, uint8_t, mavlink_message_t *last_msg);

static void mavlink_test_all(uint8_t system_id, uint8_t component_id, mavlink_message_t *last_msg)
{
	mavlink_test_common(system_id, component_id, last_msg);
	mavlink_test_control_refs(system_id, component_id, last_msg);
}
#endif

#include "../common/testsuite.h"


static void mavlink_test_control_refs(uint8_t system_id, uint8_t component_id, mavlink_message_t *last_msg)
{
	mavlink_message_t msg;
        uint8_t buffer[MAVLINK_MAX_PACKET_LEN];
        uint16_t i;
	mavlink_control_refs_t packet_in = {
		17.0,45.0,73.0,101.0
    };
	mavlink_control_refs_t packet1, packet2;
        memset(&packet1, 0, sizeof(packet1));
        	packet1.theta = packet_in.theta;
        	packet1.phi = packet_in.phi;
        	packet1.psi = packet_in.psi;
        	packet1.thrust = packet_in.thrust;
        
        

        memset(&packet2, 0, sizeof(packet2));
	mavlink_msg_control_refs_encode(system_id, component_id, &msg, &packet1);
	mavlink_msg_control_refs_decode(&msg, &packet2);
        MAVLINK_ASSERT(memcmp(&packet1, &packet2, sizeof(packet1)) == 0);

        memset(&packet2, 0, sizeof(packet2));
	mavlink_msg_control_refs_pack(system_id, component_id, &msg , packet1.theta , packet1.phi , packet1.psi , packet1.thrust );
	mavlink_msg_control_refs_decode(&msg, &packet2);
        MAVLINK_ASSERT(memcmp(&packet1, &packet2, sizeof(packet1)) == 0);

        memset(&packet2, 0, sizeof(packet2));
	mavlink_msg_control_refs_pack_chan(system_id, component_id, MAVLINK_COMM_0, &msg , packet1.theta , packet1.phi , packet1.psi , packet1.thrust );
	mavlink_msg_control_refs_decode(&msg, &packet2);
        MAVLINK_ASSERT(memcmp(&packet1, &packet2, sizeof(packet1)) == 0);

        memset(&packet2, 0, sizeof(packet2));
        mavlink_msg_to_send_buffer(buffer, &msg);
        for (i=0; i<mavlink_msg_get_send_buffer_length(&msg); i++) {
        	comm_send_ch(MAVLINK_COMM_0, buffer[i]);
        }
	mavlink_msg_control_refs_decode(last_msg, &packet2);
        MAVLINK_ASSERT(memcmp(&packet1, &packet2, sizeof(packet1)) == 0);
        
        memset(&packet2, 0, sizeof(packet2));
	mavlink_msg_control_refs_send(MAVLINK_COMM_1 , packet1.theta , packet1.phi , packet1.psi , packet1.thrust );
	mavlink_msg_control_refs_decode(last_msg, &packet2);
        MAVLINK_ASSERT(memcmp(&packet1, &packet2, sizeof(packet1)) == 0);
}

static void mavlink_test_control_refs(uint8_t system_id, uint8_t component_id, mavlink_message_t *last_msg)
{
	mavlink_test_control_refs(system_id, component_id, last_msg);
}

#ifdef __cplusplus
}
#endif // __cplusplus
#endif // CONTROL_REFS_TESTSUITE_H
