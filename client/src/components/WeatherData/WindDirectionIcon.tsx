import { ReactElement } from "react";

import { WindDirection } from "../../models/Measurement";
import { Icons } from "../Icons";
import cls from './WeatherData.module.css';

export const WindDirectionIcon = (type: WindDirection | null): ReactElement => {
  switch (type) {
    case WindDirection.EASTERN:
      return (
        <>
          <Icons.Direction className={cls.easternDirection} />
          <span>в</span>
        </>
      );
    case WindDirection.NORTHEAST:
      return (
        <>
          <Icons.Direction className={cls.northEastDirection} />;
          <span>с-в</span>
        </>
      );
    case WindDirection.NORTHERN:
      return (
        <>
          <Icons.Direction className={cls.northernDirection} />
          <span>с</span>
        </>
      );
    case WindDirection.NORTHWEST:
      return (
        <>
          <Icons.Direction className={cls.northWestDirection} />
          <span>с-з</span>
        </>
      );
    case WindDirection.SOUTH:
      return (
        <>
          <Icons.Direction className={cls.southDirection} />
          <span>ю</span>
        </>
      );
    case WindDirection.SOUTHEAST:
      return (
        <>
          <Icons.Direction className={cls.southEastDirection} />
          <span>ю-в</span>
        </>
      );
    case WindDirection.SOUTHWEST:
      return (
        <>
          <Icons.Direction className={cls.southWestDirection} />
          <span>ю-з</span>
        </>
      );
    case WindDirection.WESTERN:
      return (
        <>
          <Icons.Direction className={cls.westernDirection} />
          <span>з</span>
        </>
      );
    default:
      return <></>;
  }
};
