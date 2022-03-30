[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=nanoframework_System.Device.Adc&metric=alert_status)](https://sonarcloud.io/dashboard?id=nanoframework_System.Device.Adc) [![Reliability Rating](https://sonarcloud.io/api/project_badges/measure?project=nanoframework_System.Device.Adc&metric=reliability_rating)](https://sonarcloud.io/dashboard?id=nanoframework_System.Device.Adc) [![License](https://img.shields.io/badge/License-MIT-blue.svg)](LICENSE) [![NuGet](https://img.shields.io/nuget/dt/nanoFramework.System.Device.Adc.svg?label=NuGet&style=flat&logo=nuget)](https://www.nuget.org/packages/nanoFramework.System.Device.Adc/) [![#yourfirstpr](https://img.shields.io/badge/first--timers--only-friendly-blue.svg)](https://github.com/nanoframework/Home/blob/main/CONTRIBUTING.md) [![Discord](https://img.shields.io/discord/478725473862549535.svg?logo=discord&logoColor=white&label=Discord&color=7289DA)](https://discord.gg/gCyBu8T)

![nanoFramework logo](https://raw.githubusercontent.com/nanoframework/Home/main/resources/logo/nanoFramework-repo-logo.png)

-----

### Welcome to the .NET **nanoFramework** System.Device.Adc Library repository

## Build status

| Component | Build Status | NuGet Package |
|:-|---|---|
| System.Device.Adc | [![Build Status](https://dev.azure.com/nanoframework/System.Device.Adc/_apis/build/status/System.Device.Adc?repoName=nanoframework%2FSystem.Device.Adc&branchName=main)](https://dev.azure.com/nanoframework/System.Device.Adc/_build/latest?definitionId=83&repoName=nanoframework%2FSystem.Device.Adc&branchName=main) | [![NuGet](https://img.shields.io/nuget/v/nanoFramework.System.Device.Adc.svg?label=NuGet&style=flat&logo=nuget)](https://www.nuget.org/packages/nanoFramework.System.Device.Adc/) |

## Usage

Each target device has an ADC Controller, no matter how many ADC hardware blocks the microcontroller has.
To read a channel, one must first instantiate the ADC controller and open the channel from which it's intended to read from.
In order to read the raw value from an ADC channel, it's a simple matter of calling the Read() method on a open channel.

```csharp
AdcController adc1 = new AdcController();

AdcChannel channel0 = adc1.OpenChannel(0);

int myAdcRawvalue = channel0.ReadValue();
```

To find details about the ADC controller, one can query the various properties of the ADC controller, like this.

```csharp
// get maximum raw value from the ADC controller
int max1 = adc1.MaxValue;

// get minimum raw value from the ADC controller
int min1 = adc1.MinValue;

// find how many channels are available 
int channelCount = adc1.ChannelCount;

// resolution provided by the ADC controller
int adcResolution = adc1.ResolutionInBits;
```

## Feedback and documentation

For documentation, providing feedback, issues and finding out how to contribute please refer to the [Home repo](https://github.com/nanoframework/Home).

Join our Discord community [here](https://discord.gg/gCyBu8T).

## Credits

The list of contributors to this project can be found at [CONTRIBUTORS](https://github.com/nanoframework/Home/blob/main/CONTRIBUTORS.md).

## License

The **nanoFramework** Class Libraries are licensed under the [MIT license](LICENSE.md).

## Code of Conduct

This project has adopted the code of conduct defined by the Contributor Covenant to clarify expected behaviour in our community.
For more information see the [.NET Foundation Code of Conduct](https://dotnetfoundation.org/code-of-conduct).

### .NET Foundation

This project is supported by the [.NET Foundation](https://dotnetfoundation.org).
