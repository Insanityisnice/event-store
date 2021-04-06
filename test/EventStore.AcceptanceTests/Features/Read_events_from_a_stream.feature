@streams @read_stream
Feature: Read events from a stream
    Read the events from a stream

@missing_stream
Scenario: Read events when stream does not exist
    Given 'aggregate-1' does not exist
    Then 'aggregate-1' contains no events

@all_events
Scenario Outline: Read all events
    Given <stream> does exist
    Then <stream> contains <events>

    Scenarios:
    | scenario_name | stream      | events                         |
    | SingleEvent   | aggregate-1 | Published.Existing.SingleEvent |
    | TwoEvents     | aggregate-1 | Published.Existing.TwoEvents   |
    | ManyEvents    | aggregate-1 | Published.Existing.ManyEvents  |

@events_after_date
Scenario Outline: Read events after
    Given <stream> does exist
    Then <stream> <after> contains <events>

    Scenarios:
    | scenario_name           | stream      | after                        | events                                |
    | 2000_01_01_1400_GMT     | aggregate-1 | 2000-01-01T00:00:00+00:00:00 | Published.Existing.After_1400_GMT     |
    | 2000_01_01_Midnight_GMT | aggregate-1 | 2000-01-01T00:00:00+00:00:00 | Published.Existing.After_Midnight_GMT |
    | 2000_01_01_Midnight_EST | aggregate-1 | 2000-01-01T00:00:00-04:00:00 | Published.Existing.After_Midnight_EST |

@events_before_date
Scenario Outline: Read events before
    Given <stream> does exist
    Then <stream> <before> contains <events>

    Scenarios:
    | scenario_name           | stream      | before                        | events                                |
    | 2000_01_01_1400_GMT     | aggregate-1 | 2000-01-01T00:00:00+00:00:00 | Published.Existing.Before_1400_GMT     |
    | 2000_01_01_Midnight_GMT | aggregate-1 | 2000-01-01T00:00:00+00:00:00 | Published.Existing.Before_Midnight_GMT |
    | 2000_01_01_Midnight_EST | aggregate-1 | 2000-01-01T00:00:00-04:00:00 | Published.Existing.Before_Midnight_EST |

@events_between_dates
Scenario Outline: Read events betweem
    Given <stream> does exist
    Then <stream> between <start> <end> contains <events>

    Scenarios:
    | scenario_name                                | stream      | start                        | end                          | events                         |
    | 2000_01_01_1400__2000_02_01_1400_GMT         | aggregate-1 | 2000-01-01T00:00:00+00:00:00 | 2000-01-01T00:00:00+00:00:00 | Published.Existing.Before_1400_GMT     |
    | 2000_01_01_Midnight__2000_01_01_Midnight_GMT | aggregate-1 | 2000-01-01T00:00:00+00:00:00 | 2000-01-01T00:00:00+00:00:00 | Published.Existing.Before_Midnight_GMT |
    | 2000_01_01_Midnight__2000_01_01_Midnight_EST | aggregate-1 | 2000-01-01T00:00:00-04:00:00 | 2000-01-01T00:00:00+00:00:00 | Published.Existing.Before_Midnight_EST |