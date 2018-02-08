import moment from 'moment';

const DATE_FORMAT_STRING = 'D MMM YYYY';

export const formatDate = date => {
  return date ?  moment(date).format(DATE_FORMAT_STRING) : null;
}