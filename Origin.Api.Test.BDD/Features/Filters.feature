Feature: Filters

  Scenario: Successfully retrieve filter list from a valid file
    Given a valid filter file name "validFilter"
    When the GetFilterListFromFiles method is called with the file name "validFilter"
    Then the method should return the content of the file "validFilter.json"

  Scenario: Fail to retrieve filter list from a non-existent file
    Given a non-existent filter file name "invalidFilter"
    When the GetFilterListFromFiles method is called with the file name "invalidFilter"
    Then the method should log an error "could not get filters files for invalidFilter"
    And the method should return 404 not found http response