@streams @create_stream
Feature: Create a new stream from one or more events
    Creating a new stream when one or more events are published with required and optional values.

 @single_event
Scenario Outline: Publish events
    Given '<stream>' does not exist
    When '<events>' are published
    Then '<stream>' contains '<published_events>'

    Scenarios:
        | scenario_name          | stream       | events                                    | published_events                          |
        | OnlyRequiredProperties | aggregate-1  | ToPublish.Single.OnlyRequiredProperties   | Published.Single.OnlyRequiredProperties   |
        | AllProperties          | aggregate-1  | ToPublish.Single.AllProperties            | Published.Single.AllProperties            |
        | OnlyRequiredProperties | aggregate-1  | ToPublish.Multiple.OnlyRequiredProperties | Published.Multiple.OnlyRequiredProperties |
        | AllProperties          | aggregate-1  | ToPublish.Multiple.AllProperties          | Published.Multiple.AllProperties          |