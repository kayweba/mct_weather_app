export const getAverageTemperature = (
  temperatures: Array<number | null>
): number | null => {
  const onlyExistsTemp = temperatures.filter(
    (temperature) => temperature !== null
  );
  const sumNum = onlyExistsTemp.reduce((acc, number) => acc + number, 0);
  if (onlyExistsTemp.length > 0) return sumNum / onlyExistsTemp.length;
  return null;
};
