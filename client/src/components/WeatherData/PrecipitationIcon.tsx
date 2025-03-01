import { ReactElement } from "react";
import { PrecipitationType } from "../../models/Measurement";
import { Icons } from "../Icons";

export const PrecipitationIcon = (type: PrecipitationType | null): ReactElement => {
  switch (type) {
    case PrecipitationType.CLOUD:
      return <Icons.Cloud />;
    case PrecipitationType.SUN:
      return <Icons.Sun />;
    case PrecipitationType.RAIN:
      return <Icons.Rain />;
    case PrecipitationType.SNOW:
      return <Icons.Snow />;
    case PrecipitationType.SNOW_WITH_RAIN:
      return <Icons.SnowWithRain />;
    case PrecipitationType.PARTLY_CLOUDY:
      return <Icons.PartyCloudy />;
    default:
      return <></>;
  }
};
