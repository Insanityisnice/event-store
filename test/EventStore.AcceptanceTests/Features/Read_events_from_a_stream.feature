@streams @read_stream
Feature: Read events from a stream
    Read the events from a stream

Background: Aggregate-1 is populated
    Given Aggregate-1 contains Published.Existing.Aggregate-1

@missing_stream
Scenario: Read events when stream does not exist
    Given Aggregate-99 does not exist
    Then Aggregate-99 contains no events

@all_events
Scenario Outline: Read all events
    Then <stream> contains <events>

    Scenarios:
    | scenario_name | stream      | events                         |
    | SingleEvent   | Aggregate-1 | Published.Existing.SingleEvent |
    | TwoEvents     | Aggregate-1 | Published.Existing.TwoEvents   |
    | ManyEvents    | Aggregate-1 | Published.Existing.ManyEvents  |

@events_after_date
Scenario Outline: Read events after
    Then <stream> <after> contains <events>

    Scenarios:
    | scenario_name       | stream      | after                        | events                                       |
    | Begining_Of_A_Year  | Aggregate-1 | 2000-01-01T00:00:00+00:00:00 | Published.Existing.After_Begining_Of_A_Year  |
    | End_Of_A_Year       | Aggregate-1 | 1999-12-31T11:59:59+00:00:00 | Published.Existing.After_End_Of_A_Year       |
    | Begining_Of_A_Month | Aggregate-1 | 2000-03-01T00:00:00+00:00:00 | Published.Existing.After_Begining_Of_A_Month |
    | End_Of_A_Month      | Aggregate-1 | 2000-03-31T11:59:59+00:00:00 | Published.Existing.After_End_Of_A_Month      |

@events_before_date
Scenario Outline: Read events before
    Then <stream> <before> contains <events>

    Scenarios:
    | scenario_name       | stream      | before                       | events                                        |
    | Begining_Of_A_Year  | Aggregate-1 | 2000-01-01T00:00:00+00:00:00 | Published.Existing.Before_Begining_Of_A_Year  |
    | End_Of_A_Year       | Aggregate-1 | 1999-12-31T11:59:59+00:00:00 | Published.Existing.Before_End_Of_A_Year       |
    | Begining_Of_A_Month | Aggregate-1 | 2000-03-01T00:00:00+00:00:00 | Published.Existing.Before_Begining_Of_A_Month |
    | End_Of_A_Month      | Aggregate-1 | 2000-03-31T11:59:59+00:00:00 | Published.Existing.Before_End_Of_A_Month      |

@events_between_dates
Scenario Outline: Read events betweem
    Then <stream> between <start> <end> contains <events>

    Scenarios:
    | scenario_name                | stream      | start                        | end                          | events                                                  |
    | Beginning_And_End_Of_A_Year  | Aggregate-1 | 2000-01-01T00:00:00+00:00:00 | 2000-12-31T11:59:59+00:00:00 | Published.Existing.Between_Beginning_And_End_Of_A_Year  |
    | Beginning_And_End_Of_A_Month | Aggregate-1 | 2000-03-01T00:00:00+00:00:00 | 2000-03-31T11:59:59+00:00:00 | Published.Existing.Between_Beginning_And_End_Of_A_Month |
    | Beginning_And_End_Of_A_Day   | Aggregate-1 | 2000-01-01T00:00:00+00:00:00 | 2000-01-01T11:59:59+00:00:00 | Published.Existing.Between_Beginning_And_End_Of_A_Day   |