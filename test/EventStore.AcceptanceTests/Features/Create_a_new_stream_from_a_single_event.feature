@streams @create_stream
Feature: Create a new stream from a single event.
    Creating a new stream when a single event is published with required and optional values.

 @single_event
Scenario Outline: Publish a single event
    Given <stream> does not exist
    When <event> is published
    Then <stream> contains <published_events>

    Scenarios:
        | scenario_name          | stream       | event                                   | published_events                        |
        | OnlyRequiredProperties | aggregate-1  | ToPublish.Single.OnlyRequiredProperties | Published.Single.OnlyRequiredProperties |
        | AllProperties          | aggregate-1  | ToPublish.Single.AllProperties          | Published.Single.AllProperties          |