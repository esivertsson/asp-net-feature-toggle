## What is a feature toggle?
A FeatureToggle is a way of making code more releasable by 

1. Hiding unfinished features behind a "toggle"
2. Releasing new features gradually, by letting only a subset of users use the feature

Read Martin Fowlers blog post called [FeatureToggle](http://martinfowler.com/bliki/FeatureToggle.html), for more background. 

## Defining a FeatureToggle i web.config
```
<configuration>
  <configSections>
    <section name="featureToggle" type="AspNetFeatureToggle.Configuration.FeatureToggleSection, FeatureToggle"/>
  </configSections>
  
  <featureToggle>
    <featureList>
      <add name="AFeature" toggleOn="true" />
    </featureList>
  </featureToggle>
</configuration>
```

## Hiding unfinished features
```
if (FeatureToggle.Check("AFeature"))
{
    // Do the new stuff
}
else
{
    // Do the old stuff
}
```

## Releasing features gradually
*Will be implemented soon*

This can be done in two ways:

* Specify a list of users that can access the feature
```
<featureList>
  <add name="AFeature" toggleOn="true" userList="AFeature_Users.config" />
</featureList>
```

* Users can be randomly chosen
```
<featureList>
  <add name="AFeature" toggleOn="true" randomFactor="0.1" />
</featureList>
```
