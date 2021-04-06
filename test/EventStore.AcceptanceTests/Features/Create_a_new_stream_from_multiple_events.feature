@streams @create_stream
Feature: Create a new stream from multiple event.
    Creating a new stream when multiple events are published with required and optional values.

@multiple_events
Scenario Outline: Publish a multiple events
    Given <stream> does not exist
    When <events> are published
    Then <stream> contains <published_events>

    Scenarios:
        | scenario_name          | stream       | event                          | published_event                |
        | OnlyRequiredProperties | aggregate-1  | ToPublish.Multiple.Aggregate-1 | Published.Multiple.Aggregate-1 |
        | AllProperties          | aggregate-2  | ToPublish.Multiple.Aggregate-2 | Published.Multiple.Aggregate-2 |