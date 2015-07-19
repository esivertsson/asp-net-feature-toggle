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
      <add name="AFeature" isEnabled="true" />
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
This can be done in two ways:

* Specify a list of users that can access the feature
```
<featureList>
  <add name="AFeature" isEnabled="true" userListPath="AFeature_Users.config" />
</featureList>

if (FeatureToggle.Check(<feature name>, <user name>))
{
    // Do the new stuff
}
else
{
    // Do the old stuff
}
```


* Users can be randomly chosen, for example for A/B testing or canary releasing
```
<featureList>
  <add name="AFeature" isEnabled="true" randomFactor="0.1" />
</featureList>
```
randomFactor = 0.1 means 10% of requests will have the feature enabled.
0.5 is 50% and so on.


## Useful lessons learned about feature toggles from this [InfoQ talk] (http://www.infoq.com/presentations/Feature-Bits)
* Limit the number of FeatureToggles 
* A FeatureToggle should have a short lifetime
* Have as few places as possible where code is wrapped, preferrably only one per FeatureToggle.
* Use a naming convention 