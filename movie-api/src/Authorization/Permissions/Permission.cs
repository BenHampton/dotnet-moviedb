namespace movie_api.Authorization.Permissions;

public enum Permission
{
    ADMINISTRATOR,

    // Schedules
    SCHEDULE_CREATE,
    SCHEDULE_READ,
    SCHEDULE_UPDATE,
    SCHEDULE_DELETE,
    SCHEDULE_POST,
    SCHEDULE_BUILD,
    // Schedules

    // Schedule Notes
    SCHEDULE_NOTE_CREATE,
    SCHEDULE_NOTE_READ,
    SCHEDULE_NOTE_UPDATE,
    SCHEDULE_NOTE_DELETE,
    // Schedule Notes

    // Line Maintenance
    LINE_MAINTENANCE_CREATE,
    LINE_MAINTENANCE_READ,
    LINE_MAINTENANCE_UPDATE,
    LINE_MAINTENANCE_DELETE,
    // Line Maintenance

    // Tracker
    TRACKER_CREATE,
    TRACKER_READ,
    TRACKER_UPDATE,
    TRACKER_DELETE,
    // Tracker

    // Controller Schedule
    CONTROLLER_SCHEDULE_CREATE,
    CONTROLLER_SCHEDULE_READ,
    CONTROLLER_SCHEDULE_UPDATE,
    CONTROLLER_SCHEDULE_DELETE,
    CONTROLLER_SCHEDULE_POST,
    CONTROLLER_SCHEDULE_BUILD,
    // Controller Schedule

    // Line Control
    LINE_SPEED_UP,
    LINE_SLOW_DOWN,
    LINE_SHUT_DOWN,
    LINE_CREATE_CHAIN,
    LINE_CREATE_LINK,
    // Line Control

    // Sequence Editor
    SEQUENCE_MODIFY_TANKAGE_PARTY,
    // TODO: Figure out all the actions for sequences
    // Sequence Editor

    // Conflicts
    CONFLICTS_VIEW,
    CONFLICTS_MANAGE,
    // Conflicts
}
